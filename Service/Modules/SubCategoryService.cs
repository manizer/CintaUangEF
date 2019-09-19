using Helper.Object;
using Model.Domain.DB;
using Model.DTO.DB;
using Model.DTO.DB.SubCategoryDB;
using Repository.Context;
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
		private readonly DbContextFactory dbContextFactory;

		public SubCategoryService(ISubCategoryRepository subCategoryRepository,
			ICategoryRepository categoryRepository,
			DbContextFactory dbContextFactory)
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
				CategoryId = x.CategoryId,
				Category = new Lazy<Category>(() =>
				{
					using (var context = dbContextFactory.GetContext())
					{
						subCategoryRepository.UseContext(context);
						Category category = (new Category()).CopyPropertiesFrom(categoryRepository.GetCategory(x.CategoryId));
						return category;
					}
				})
			});

		public ExecuteResult InsertSubCategory(InsertSubCategory insertSubCategory)
		{
			SubCategoryDTO subCategoryDTO = new SubCategoryDTO().CopyPropertiesFrom(insertSubCategory);
			ExecuteResultDTO executeResultDTO = subCategoryRepository.InsertSubCategory(subCategoryDTO);
			return new ExecuteResult().CopyPropertiesFrom(executeResultDTO);
		}

		public ExecuteResult UpdateSubCategory(UpdateSubCategory updateSubCategory)
		{
			SubCategoryDTO subCategoryDTO = new SubCategoryDTO().CopyPropertiesFrom(updateSubCategory);
			ExecuteResultDTO executeResultDTO = subCategoryRepository.UpdateSubCategory(subCategoryDTO);
			return new ExecuteResult
			{
				InstanceId = executeResultDTO.InstanceId
			};
		}
	}
}
