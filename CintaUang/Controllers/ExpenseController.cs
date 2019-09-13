using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CintaUang.ViewModels.ExpenseViewModels.Components;
using Microsoft.AspNetCore.Mvc;

namespace CintaUang.Controllers
{
    public class ExpenseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		#region ViewComponent
		public IActionResult InsertUpdateExpenseOnSubmit(InsertUpdateExpenseViewModel insertUpdateExpenseViewModel)
		{
			return ViewComponent("InsertUpdateExpense", insertUpdateExpenseViewModel.ExpenseId);
		}
		#endregion
	}
}