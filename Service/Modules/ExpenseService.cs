using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.CategoryRepositories;
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
	}

	public class ExpenseService : IExpenseService
	{
		private readonly ICategoryRepository categoryRepository;
		private readonly ISubCategoryRepository subCategoryRepository;
		private readonly DbContextFactory dbContextFactory;

		public ExpenseService(ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository,
			DbContextFactory dbContextFactory)
		{
			this.categoryRepository = categoryRepository;
			this.subCategoryRepository = subCategoryRepository;
			this.dbContextFactory = dbContextFactory;
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
	}
}
