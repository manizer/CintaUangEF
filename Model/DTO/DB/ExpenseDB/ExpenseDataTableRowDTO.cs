using Model.DTO.DB.DataTable.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.DTO.DB.ExpenseDB
{
	public class ExpenseDataTableRowDTO: AjaxDataTableRowDTO
	{
		[Key]
		public int ExpenseId { get; set; }
		public string ExpenseName { get; set; }
		public int Amount { get; set; }
		public string SubCategoryName { get; set; }
		public string CategoryName { get; set; }
	}
}
