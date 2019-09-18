using Microsoft.EntityFrameworkCore;
using Model.Domain.DB.DataTable;
using Model.DTO.DB.ExpenseDB;
using Repository.Context;
using Repository.Repositories.CategoryRepositories;
using Repository.Repositories.ExpenseRepositories;
using Repository.Repositories.SubCategoryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Domains.ExpenseDomains.ExpenseDomain;

namespace Service.Modules
{
	public interface IExpenseService
	{
		IEnumerable<Category> GetCategories();
		AjaxDataTable<ExpenseDataTableRow> GetExpenseDataTableRows(int Page, int Take, string Search);
	}

	public class ExpenseService : IExpenseService
	{
		private readonly ICategoryRepository categoryRepository;
		private readonly ISubCategoryRepository subCategoryRepository;
		private readonly IExpenseDatatableRepository expenseDatatableRepository;
		private readonly DbContextFactory dbContextFactory;

		public ExpenseService(ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository,
			DbContextFactory dbContextFactory,
			IExpenseDatatableRepository expenseDatatableRepository)
		{
			this.categoryRepository = categoryRepository;
			this.subCategoryRepository = subCategoryRepository;
			this.dbContextFactory = dbContextFactory;
			this.expenseDatatableRepository = expenseDatatableRepository;
		}

		public IEnumerable<Category> GetCategories() => categoryRepository.GetCategories()
			.Select(x => new Category
			{
				Id = x.Id,
				Name = x.Name,
				SubCategories = new Lazy<List<SubCategory>>(() =>
				{
					using (var context = dbContextFactory.GetContext())
					{
						subCategoryRepository.UseContext(context);
						var subcategories = subCategoryRepository.GetSubCategoriesByCategoryID(x.Id)
							.Select(s => new SubCategory
							{
								Id = s.Id,
								CategoryId = s.CategoryId,
								Name = s.Name
							})?.ToList();
						return subcategories;
					}
				})
			});

		public AjaxDataTable<ExpenseDataTableRow> GetExpenseDataTableRows(int Page, int Take, string Search)
		{
			IEnumerable<ExpenseDataTableRow> expenseDataTableRows = expenseDatatableRepository.GetExpenseDataTableRows(Page, Take, Search)
				.Select(x => new ExpenseDataTableRow
				{
					Amount = x.Amount,
					CategoryName = x.CategoryName,
					CurrentRecord = x.CurrentRecord,
					ExpenseId = x.ExpenseId,
					ExpenseName = x.ExpenseName,
					SubCategoryName = x.SubCategoryName,
					TotalPage = x.TotalPage,
					TotalRecord = x.TotalRecord
				});

			return new AjaxDataTable<ExpenseDataTableRow>
			{
				Data = expenseDataTableRows.ToList(),
				RecordsFiltered = expenseDataTableRows.FirstOrDefault()?.TotalPage,
				RecordsTotal = expenseDataTableRows.FirstOrDefault()?.TotalRecord ?? 0
			};
		}
			
	}
}
