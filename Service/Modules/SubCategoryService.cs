using Model.Domain.DB;
using Model.DTO.DB;
using Model.DTO.DB.SubCategoryDB;
using Repository.Repositories.CategoryRepositories;
using Repository.Repositories.SubCategoryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Domains.SubCategoryDomains.SubCategoryDomain;

namespace Service.Modules
{
	public interface ISubCategoryService
	{
		IEnumerable<Category> GetCategories();
		IEnumerable<SubCategory> GetSubcategoriesByCategoryID(int CategoryID);
		ExecuteResult InsertSubCategory(InsertSubCategory insertSubCategory);
		ExecuteResult UpdateSubCategory(UpdateSubCategory updateSubCategory);
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

		public IEnumerable<Category> GetCategories() => categoryRepository.GetCategories()
			.Select(x => new Category
			{
				Id = x.Id,
				Name = x.Name
			});

		public IEnumerable<SubCategory> GetSubcategoriesByCategoryID(int CategoryID) => subCategoryRepository.GetSubCategoriesByCategoryID(CategoryID)
			.Select(x => new SubCategory
			{
				Id = x.Id,
				Name = x.Name,
				CategoryId = x.CategoryId
			});

		public ExecuteResult InsertSubCategory(InsertSubCategory insertSubCategory)
		{
			InsertSubCategoryDTO insertSubCategoryDTO = new InsertSubCategoryDTO
			{
				CategoryId = insertSubCategory.CategoryId,
				AuditedUserId = insertSubCategory.AuditedUserId,
				SubCategoryName = insertSubCategory.SubCategoryName
			};

			ExecuteResultDTO executeResultDTO = subCategoryRepository.InsertSubCategory(insertSubCategoryDTO);
			return new ExecuteResult
			{
				InstanceId = executeResultDTO.InstanceId
			};
		}

		public ExecuteResult UpdateSubCategory(UpdateSubCategory updateSubCategory)
		{
			UpdateSubCategoryDTO updateSubCategoryDTO = new UpdateSubCategoryDTO
			{
				CategoryId = updateSubCategory.CategoryId,
				AuditedUserId = updateSubCategory.AuditedUserId,
				SubCategoryName = updateSubCategory.SubCategoryName,
				SubCategoryId = updateSubCategory.SubCategoryId
			};

			ExecuteResultDTO executeResultDTO = subCategoryRepository.UpdateSubCategory(updateSubCategoryDTO);
			return new ExecuteResult
			{
				InstanceId = executeResultDTO.InstanceId
			};
		}
	}
}
