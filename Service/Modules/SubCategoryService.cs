using Model.Domain.DB;
using Model.Domain.DB.CategoryDB;
using Model.Domain.DB.SubCategoryDB;
using Model.DTO.DB.SubCategoryDB;
using Repository.Repositories.CategoryRepositories;
using Repository.Repositories.SubCategoryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Modules
{
	public interface ISubCategoryService
	{
		Task<IEnumerable<Category>> GetCategories();
		Task<IEnumerable<SubCategory>> GetSubcategoriesByCategoryID(int CategoryID);
        Task<ExecuteResult> InsertSubCategory(InsertSubCategory insertSubCategory);
        Task<ExecuteResult> UpdateSubCategory(UpdateSubCategory updateSubCategory);
    }

	public class SubCategoryService : ISubCategoryService
	{
		private readonly ISubCategoryRepository subCategoryRepository;
		private readonly ICategoryRepository categoryRepository;

		public SubCategoryService(ISubCategoryRepository subCategoryRepository,
			ICategoryRepository categoryRepository)
		{
			this.subCategoryRepository = subCategoryRepository;
			this.categoryRepository = categoryRepository;
		}

		public async Task<IEnumerable<Category>> GetCategories() => await categoryRepository.GetCategories();
		public async Task<IEnumerable<SubCategory>> GetSubcategoriesByCategoryID(int CategoryID) => await subCategoryRepository.GetSubCategoriesByCategoryID(CategoryID);

        public async Task<ExecuteResult> InsertSubCategory(InsertSubCategory insertSubCategory)
        {
            return await subCategoryRepository.InsertSubCategory(new InsertSubCategoryDTO
            {
                CategoryId = insertSubCategory.CategoryId,
                SubCategoryName = insertSubCategory.SubCategoryName,
                AuditedUserId = insertSubCategory.AuditedUserId
            });
        }

        public async Task<ExecuteResult> UpdateSubCategory(UpdateSubCategory updateSubCategory)
        {
            return await subCategoryRepository.UpdateSubCategory(new UpdateSubCategoryDTO
            {
                SubCategoryId = updateSubCategory.SubCategoryId,
                CategoryId = updateSubCategory.CategoryId,
                SubCategoryName = updateSubCategory.SubCategoryName,
                AuditedUserId = updateSubCategory.AuditedUserId,
            });
        }
    }
}
