using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.Domain.DB;
using Model.Domain.DB.CategoryDB;
using Model.Domain.DB.SubCategoryDB;
using Model.DTO.DB;
using Model.DTO.DB.CategoryDB;
using Repository.Base;
using Repository.Base.Helper;
using Repository.Base.Helper.StoredProcedure;
using Repository.Context;
using Repository.Repositories.SubCategoryRepositories;

namespace Repository.Repositories.CategoryRepositories
{
    public interface ICategoryRepository : IRepository<CategoryDTO>
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task<ExecuteResult> InsertCategory(InsertCategoryDTO insertCategoryDTO);
        Task<ExecuteResult> UpdateCategory(UpdateCategoryDTO insertCategoryDTO);
	}

    public class CategoryRepository : BaseRepository<CategoryDTO>, ICategoryRepository
	{ 
		private readonly ISubCategoryRepository subCategoryRepository;
		private readonly DbContextOptions<CintaUangDbContext> options;

        public CategoryRepository(CintaUangDbContext dbContext, DbUtil dbUtil,
			ISubCategoryRepository subCategoryRepository,
			DbContextOptions<CintaUangDbContext> options,
			UnitOfWork unitOfWork) : base(dbContext, dbUtil)
		{
			this.subCategoryRepository = subCategoryRepository;
			this.options = options;
		}

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var sp = DbUtil.StoredProcedureBuilder.WithSPName("mscategory_getall").SP();
            IEnumerable<CategoryDTO> categoryDTOs = await ExecSPToListAsync(sp);
			IEnumerable<Category> categories = categoryDTOs.Select(x => new Category
			{
				Id = x.Id,
				Name = x.Name,
				Subcategories = _SubCategories(x.Id)
			});
			return categories;
        }

		public async Task<Category> GetCategory(int id)
        {
            var sp = DbUtil.StoredProcedureBuilder.WithSPName("mscategory_getbyid").AddParam(id).SP();
            var categoryDTO = await ExecSPToSingleAsync(sp);
			return new Category
			{
				Id = categoryDTO.Id,
				Name = categoryDTO.Name,
				Subcategories = _SubCategories(id)
			};
		}

        public async Task<ExecuteResult> InsertCategory(InsertCategoryDTO category)
        {
            List<StoredProcedure> storedProcedures = new List<StoredProcedure>();
            storedProcedures.Add(
                DbUtil.StoredProcedureBuilder.WithSPName("mscategory_insert")
                    .AddParam(category.Name) 
                    .AddParam(category.AuditedUserId)
                    .SP()
            );
			IEnumerable<ExecuteResultDTO> executeResults = await ExecSPWithTransaction<ExecuteResultDTO>(storedProcedures.ToArray());
			return executeResults.Select(x => new ExecuteResult
			{
				InstanceId = x.InstanceId,
			}).FirstOrDefault();
        }

		public async Task<ExecuteResult> UpdateCategory(UpdateCategoryDTO category)
		{
			List<StoredProcedure> storedProcedures = new List<StoredProcedure>();
			storedProcedures.Add(
				DbUtil.StoredProcedureBuilder.WithSPName("mscategory_update")
					.AddParam(category.Id)
					.AddParam(category.Name)
					.AddParam(category.AuditedUserId)
					.SP()
			);
			IEnumerable<ExecuteResultDTO> executeResults = await ExecSPWithTransaction<ExecuteResultDTO>(storedProcedures.ToArray());
			return executeResults.Select(x => new ExecuteResult
			{
				InstanceId = x.InstanceId,
			}).FirstOrDefault();
		}

		#region DB Mapping
		private Lazy<List<SubCategory>> _SubCategories(int CategoryID)
		{
			return new Lazy<List<SubCategory>>(() =>
			{
				using (var context = new CintaUangDbContext(options))
				{
					//do work
					subCategoryRepository.UseContext(context);
					return subCategoryRepository.GetSubCategoriesByCategoryID(CategoryID).Result.ToList();
				}
			});
		}
		#endregion
	}
}