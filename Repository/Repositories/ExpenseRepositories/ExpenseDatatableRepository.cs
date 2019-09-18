using Model.DTO.DB.ExpenseDB;
using Repository.Base;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Repositories.ExpenseRepositories
{
	public interface IExpenseDatatableRepository : IRepository<ExpenseDataTableRowDTO>
	{
		IEnumerable<ExpenseDataTableRowDTO> GetExpenseDataTableRows(int Page, int Take, string Search);
	}

	public class ExpenseDatatableRepository : BaseRepository<ExpenseDataTableRowDTO>, IExpenseDatatableRepository
	{
		public ExpenseDatatableRepository(CintaUangDbContext dbContext) : base(dbContext)
		{
		}

		public IEnumerable<ExpenseDataTableRowDTO> GetExpenseDataTableRows(int Page, int Take, string Search)
		{
			var entries = (from a in Context.Expenses
						   join b in Context.SubCategories on a.SubcategoryId equals b.Id
						   join c in Context.Categories on b.CategoryId equals c.Id
						   where a.Name.Contains(Search) &&
						   b.Name.Contains(Search) &&
						   c.Name.Contains(Search)
						   orderby a.Id
						   select new
						   {
							   ExpenseId = a.Id,
							   ExpenseName = a.Name,
							   Amount = a.Amount,
							   SubCategoryName = b.Name,
							   CategoryName = c.Name
						   });
			var TotalEntries = entries.Count();
			var TotalPage = Convert.ToInt32(TotalEntries / Take);
			var Paginated = entries
							.Take(Take)
							.Skip((Page - 1) * Take)
							.ToList();

			return Paginated.Select(x => new ExpenseDataTableRowDTO
			{
				Amount = x.Amount,
				CategoryName = x.CategoryName,
				SubCategoryName = x.SubCategoryName,
				ExpenseId = x.ExpenseId,
				ExpenseName = x.ExpenseName,
				TotalPage = TotalPage,
				TotalRecord = TotalEntries,
				CurrentRecord = 0
			});
		}
	}
}
