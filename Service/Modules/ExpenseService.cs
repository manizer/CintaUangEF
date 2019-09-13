using Model.Domain.DB.CategoryDB;
using Repository.Repositories.CategoryRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Modules
{
	public interface IExpenseService
	{
		Task<IEnumerable<Category>> GetCategories();
	}

	public class ExpenseService : IExpenseService
	{
		private readonly ICategoryRepository categoryRepository;

		public ExpenseService(ICategoryRepository categoryRepository)
		{
			this.categoryRepository = categoryRepository;
		}

		public async Task<IEnumerable<Category>> GetCategories() => await categoryRepository.GetCategories();
	}
}
