using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Lib.DropdownLibs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CintaUang.ViewModels.ExpenseViewModels.Components
{
	public class InsertUpdateExpenseViewModel
	{
		#region Form Data
		public int ExpenseId { get; set; }
		public List<SelectListItem> CategorySelectListItems { get; set; }
		public List<SelectListItem> SubCategorySelectListItems { get; set; }
		public List<DropdownItem> CategoryDropdown { get; set; }
		public int CategoryId { get; set; }
		public int SubCategoryId { get; set; }

		public string ExpenseName { get; set; }
		public int Amount { get; set; }
		#endregion
	}
}
