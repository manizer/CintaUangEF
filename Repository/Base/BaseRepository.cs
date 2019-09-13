using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Base.Helper;
using Repository.Base.Helper.StoredProcedure;
using Repository.Context;

namespace Repository.Base
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected CintaUangDbContext Context;
        private CintaUangDbContext PrevDbContext;
        protected readonly DbUtil DbUtil;

        public BaseRepository(CintaUangDbContext context, DbUtil dbUtil)
        {
            Context = context;
            DbUtil = dbUtil;
        }

        public async Task<IEnumerable<TEntity>> ExecSPToListAsync(StoredProcedure sp)
        {
            Task<IEnumerable<TEntity>> Result;
            try
            {
                Result = Task.Run(() =>
                {
                    return Context.Set<TEntity>().FromSql<TEntity>(sp.SP, sp.args).ToList().AsEnumerable();
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
            finally
            {
                DbUtil.DisposeAll();
            }
            return await Result;
        }

		public async Task<TEntity> ExecSPToSingleAsync(StoredProcedure sp)
        {
            Task<TEntity> Result = null;
            try
            {
                Result = Task.Run(() =>
                {
                    return Context.Set<TEntity>().FromSql<TEntity>(sp.SP, sp.args).ToList().FirstOrDefault();
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
            finally
            {
                DbUtil.DisposeAll();
            }
            return await Result;
        }

        public async Task<IEnumerable<TEntityResult>> ExecSPWithTransaction<TEntityResult>(params StoredProcedure[] storedProcedures) where TEntityResult : class
        {
            List<TEntityResult> ExecuteResults = new List<TEntityResult>();
            try
            {
                if (PrevDbContext == null) Context.Database.BeginTransaction();
                foreach (StoredProcedure sp in storedProcedures)
                {
                    TEntityResult executeResult = Context.Set<TEntityResult>().FromSql<TEntityResult>(sp.SP, sp.args).ToList().FirstOrDefault();
                    ExecuteResults.Add(executeResult);
                }
                if (PrevDbContext == null) Context.Database.CommitTransaction();
            }
            catch (Exception e)
            {
                if (PrevDbContext == null) Context.Database.RollbackTransaction();
                throw e;
            }
            finally
            {
                DbUtil.DisposeAll();
            }

            return await Task.FromResult(ExecuteResults.AsEnumerable());
        }

		public void UseContext(CintaUangDbContext context)
        {
            PrevDbContext = this.Context;
            this.Context = context;
        }

        public void RevertToPreviousDbContext()
        {
            this.Context = PrevDbContext;
            PrevDbContext = null;
        }
    }
}