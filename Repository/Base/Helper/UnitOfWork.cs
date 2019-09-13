using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Repository.Context;

namespace Repository.Base.Helper
{
    public class UnitOfWork
    {
        private readonly IServiceProvider serviceProvider;

        public UnitOfWork(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Encapsulates several repository calls with one DbContext. Any exceptions thrown inside will end up rolling back all repository transactions.
        ///
        /// To change a repository's context to use unitOfWork's localContext, make sure to call
        /// <c>rRegistrar.ConvertContextOfRepository(myRepo).ToUse(localContext);</c>
        /// </summary>
        /// 
        /// <param name="repoWork">Asynchronous lambda to do the repository transactions</param>
        public async Task Run(RepoWork repoWork)
        {
            RepositoryRegistry rRegistry = new RepositoryRegistry();
            RepositoryRegistryRegistrar rRegistrar = new RepositoryRegistryRegistrar(rRegistry);
            CintaUangDbContext localContext = null;
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    localContext = scope.ServiceProvider.GetService<CintaUangDbContext>();
                    localContext.Database.BeginTransaction();

                    await repoWork(rRegistrar, localContext);
                    localContext.Database.CommitTransaction();
                }
            }
            catch (Exception e)
            {
                localContext.Database.RollbackTransaction();
                throw e;
            }
            finally
            {
                /**
                 * Convert All DbContexts of all repositories registered in registry
                 */
                rRegistry.GetRepositories().ForEach(unitOfWorkRepository => unitOfWorkRepository.RevertToPreviousDbContext());
            }
        }

		public delegate Task RepoWork(RepositoryRegistryRegistrar rRegistrar, CintaUangDbContext context);
    }

    /// <summary>
    /// Provides a fluent API to register and change a repository's dbContext to localContext provided by UnitOfWork
    /// </summary>
    public class RepositoryRegistryRegistrar
    {
        RepositoryRegistry repositoryRegistry;

        public RepositoryRegistryRegistrar(RepositoryRegistry repositoryRegistry)
        {
            this.repositoryRegistry = repositoryRegistry;
        }

        public RepositoryContextAdapter ConvertContextOfRepository(IUnitOfWorkRepository repository)
        {
            return new RepositoryContextAdapter(repository, repositoryRegistry);
        }
    }

    /// <summary>
    /// Provides a fluent API to apply change of to a repository's context and record changed repositories into registry
    /// </summary>
    public class RepositoryContextAdapter
    {
        IUnitOfWorkRepository repository;
        RepositoryRegistry repositoryRegistry;

        public RepositoryContextAdapter(IUnitOfWorkRepository repository, RepositoryRegistry repositoryRegistry)
        {
            this.repository = repository;
            this.repositoryRegistry = repositoryRegistry;
        }

        public void ToUse(CintaUangDbContext context)
        {
            repositoryRegistry.AddRepo(repository);
            repository.UseContext(context);
        }
    }

    /// <summary>
    /// Keeps track of modified repositories
    /// </summary>
    public class RepositoryRegistry
    {
        private List<IUnitOfWorkRepository> Repositories = new List<IUnitOfWorkRepository>();
        public void AddRepo(IUnitOfWorkRepository repository) { Repositories.Add(repository); }
        public List<IUnitOfWorkRepository> GetRepositories() { return Repositories; }
    }
}
