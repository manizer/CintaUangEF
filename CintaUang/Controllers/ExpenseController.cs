using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CintaUang.ViewModels.ExpenseViewModels.Components;
using Microsoft.AspNetCore.Mvc;
using Model.Domain.DB.DataTable;
using Service.Modules;
using static Model.Domains.ExpenseDomains.ExpenseDomain;

namespace CintaUang.Controllers
{
    public class ExpenseController : Controller
    {
		private readonly IExpenseService expenseService;

		public ExpenseController(IExpenseService expenseService)
		{
			this.expenseService = expenseService;
		}

        public IActionResult Index()
        {
            return View();
        }

		public JsonResult ExpenseDataTable(int draw, int start, int length)
		{
			int Page = (start / length) + 1;
			AjaxDataTable<ExpenseDataTableRow> ajaxDataTable = expenseService.GetExpenseDataTableRows(Page, length, "");
			ajaxDataTable.Draw = draw;
			return Json(ajaxDataTable);
		}

		#region ViewComponent
		public IActionResult InsertUpdateExpenseOnSubmit(InsertUpdateExpenseViewModel insertUpdateExpenseViewModel)
		{
			return ViewComponent("InsertUpdateExpense", insertUpdateExpenseViewModel.ExpenseId);
		}
		#endregion
	}
}