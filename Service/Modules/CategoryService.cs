using Helper.Object;
using Model.Domain.DB;
using Model.Domain.DB.DataTable;
using Model.DTO.DB;
using Model.DTO.DB.CategoryDB;
using Model.DTO.DB.DataTable;
using Model.DTO.DB.SubCategoryDB;
using Repository.Base.Helper;
using Repository.Repositories.CategoryRepositories;
using Repository.Repositories.SubCategoryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Domains.CategoryDomains.CategoryDomain;

namespace Service.Modules
{
	public interface ICategoryService
	{
		IEnumerable<Category> GetCategories();
		Category GetCategory(int CategoryId);
		ExecuteResult Insert(InsertCategory insertCategory);
		ExecuteResult Update(UpdateCategory insertCategory);
		ExecuteResult Delete(int Id);

		void UnitOfWorkSample();
	}

	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository categoryRepository;
		private readonly ISubCategoryRepository subCategoryRepository;
		private readonly UnitOfWork unitOfWork;

		public CategoryService(ICategoryRepository categoryRepository,
			ISubCategoryRepository subCategoryRepository,
			UnitOfWork unitOfWork)
		{
			this.categoryRepository = categoryRepository;
			this.subCategoryRepository = subCategoryRepository;
			this.unitOfWork = unitOfWork;
		}

		public IEnumerable<Category> GetCategories()
		{
			IEnumerable<CategoryDTO> categoryDtos = categoryRepository.GetCategories();
            return categoryDtos.Select(x => new Category().CopyPropertiesFrom(x));
		}

		public Category GetCategory(int CategoryId)
		{
			CategoryDTO categoryDTO = categoryRepository.GetCategory(CategoryId);
            return new Category().CopyPropertiesFrom(categoryDTO);
		}

		public ExecuteResult Insert(InsertCategory insertCategory)
		{
			ExecuteResultDTO executeResultDTO = categoryRepository.InsertCategory(new CategoryDTO
			{
				Name = insertCategory.Name
			});
			return new ExecuteResult
			{
				InstanceId = executeResultDTO.InstanceId
			};
		}

		public ExecuteResult Update(UpdateCategory updateCategory)
		{
            ExecuteResultDTO executeResultDTO = categoryRepository.UpdateCategory(new CategoryDTO().CopyPropertiesFrom(updateCategory));
            return new ExecuteResult().CopyPropertiesFrom(executeResultDTO);
		}

		public ExecuteResult Delete(int Id) 
		{
			ExecuteResultDTO executeResultDTO = categoryRepository.DeleteCategory(Id);
            return new ExecuteResult().CopyPropertiesFrom(executeResultDTO);
		}

		public void UnitOfWorkSample()
		{
			try
			{
				unitOfWork.Run((r, ctx) =>
				{
					r.ConvertContextOfRepository(subCategoryRepository).ToUse(ctx);
					r.ConvertContextOfRepository(categoryRepository).ToUse(ctx);

					categoryRepository.InsertCategory(new CategoryDTO
					{
						Name = "FAIL-CAT"
					});
					
					subCategoryRepository.InsertSubCategory(new SubCategoryDTO
					{
						Name = "FAIL-SUB",
						CategoryId = 1
					});
				});
			}
			catch(Exception e)
			{

			}
		}
	}
}
