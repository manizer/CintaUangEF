using Helper.Object;
using Model.Domain.DB;
using Model.Domain.DB.DataTable;
using Model.DTO.DB;
using Model.DTO.DB.CategoryDB;
using Model.DTO.DB.DataTable;
using Repository.Repositories.CategoryRepositories;
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
		Task<AjaxDataTable<CategoryDataTableRow>> GetCategoryDataTable(int Page, int Take, string Search, int OrderColIdx, string OrderDirection);
		ExecuteResult Insert(InsertCategory insertCategory);
		ExecuteResult Update(UpdateCategory insertCategory);
		ExecuteResult Delete(int Id);
	}

	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository categoryRepository;
		private readonly ICategoryDataTableRepository categoryDataTableRepository;
		private readonly IServiceProvider serviceProvider;

		public CategoryService(ICategoryRepository categoryRepository,
			ICategoryDataTableRepository categoryDataTableRepository,
			IServiceProvider serviceProvider)
		{
			this.categoryRepository = categoryRepository;
			this.categoryDataTableRepository = categoryDataTableRepository;
			this.serviceProvider = serviceProvider;
		}

		public IEnumerable<Category> GetCategories()
		{
			IEnumerable<CategoryDTO> categoryDtos = categoryRepository.GetCategories();
            List<Category> categories = new List<Category>();
            foreach (var categoryDto in categoryDtos)
            {
                categories.Add(new Category().CopyPropertiesFrom(categoryDto));
            }
            return categories;
		}

		public Category GetCategory(int CategoryId)
		{
			CategoryDTO categoryDTO = categoryRepository.GetCategory(CategoryId);
            return new Category().CopyPropertiesFrom(categoryDTO);
		}

		public async Task<AjaxDataTable<CategoryDataTableRow>> GetCategoryDataTable(int Page, int Take, string Search, int OrderColIdx, string OrderDirection)
		{
			AjaxDataTableDTO<CategoryDataTableRowDTO> categoryAjaxDataTableDTO = await categoryDataTableRepository.GetCategoryDataTable(Page, Take, Search, OrderColIdx, OrderDirection);
			AjaxDataTable<CategoryDataTableRow> categoryAjaxDataTable = new AjaxDataTable<CategoryDataTableRow>
			{
				Draw = categoryAjaxDataTableDTO.Draw,
				RecordsFiltered = categoryAjaxDataTableDTO.RecordsFiltered,
				RecordsTotal = categoryAjaxDataTableDTO.RecordsTotal,
				Data = categoryAjaxDataTableDTO.Data.Select(x => new CategoryDataTableRow
				{
					Id = x.Id,
					Name = x.Name
				}).ToList()
			};
			return categoryAjaxDataTable;
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
			ExecuteResultDTO executeResultDTO = categoryRepository.UpdateCategory(new CategoryDTO
			{
				Id = updateCategory.Id,
				Name = updateCategory.Name
			});
			return new ExecuteResult
			{
				InstanceId = executeResultDTO.InstanceId
			};
		}

		public ExecuteResult Delete(int Id)
		{
			ExecuteResultDTO executeResultDTO = categoryRepository.DeleteCategory(Id);
			return new ExecuteResult
			{
				InstanceId = executeResultDTO.InstanceId
			};
		}
	}
}
