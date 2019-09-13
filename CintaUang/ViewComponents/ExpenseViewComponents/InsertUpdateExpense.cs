﻿using CintaUang.Helpers.ViewComponentHelpers;
using CintaUang.ViewModels.ExpenseViewModels.Components;
using Microsoft.AspNetCore.Mvc;
using Model.Domain.DB.CategoryDB;
using Model.Lib.DropdownLibs;
using Service.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CintaUang.ViewComponents.ExpenseViewComponents
{
	public class InsertUpdateExpense: ViewComponent
	{
		private readonly IExpenseService expenseService;

		public InsertUpdateExpense(IExpenseService expenseService)
		{
			this.expenseService = expenseService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			List<Category> categories = (await expenseService.GetCategories()).ToList();
			return View(ViewComponentPath.ViewPath("Expense", "_InsertUpdateExpense"), new InsertUpdateExpenseViewModel
			{
				CategorySelectListItems = Dropdown.From(categories),
				CategoryDropdown = categories.ToList<DropdownItem>()
			});
		}
	}
}
