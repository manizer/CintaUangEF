using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO.DB.ExpenseDB
{
	public class ExpenseDTO
	{
		public int Id { get; set; }
		public int SubcategoryId { get; set; }
		public string Name { get; set; }
		public int Amount { get; set; }
	}
}
