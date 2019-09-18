using CintaUang.Helpers.ViewComponentHelpers;
using Microsoft.AspNetCore.Mvc;
using Service.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Model.Domains.ExpenseDomains.ExpenseDomain;

namespace CintaUang.ViewComponents.ExpenseViewComponents
{
	public class ExpenseDataTable : ViewComponent
	{
		private readonly IExpenseService expenseService;

		public ExpenseDataTable(IExpenseService expenseService)
		{
			this.expenseService = expenseService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View(ViewComponentPath.ViewPath("Expense", "_ExpenseDataTable"));
		}
	}
}
