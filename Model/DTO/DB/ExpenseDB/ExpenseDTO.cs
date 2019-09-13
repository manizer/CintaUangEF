using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.DTO.DB.ExpenseDB
{
	[Table("trexpense")]
	public class ExpenseDTO
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }
		[Column("subcategoryid")]
		public int SubcategoryId { get; set; }
		[Column("name")]
		public string Name { get; set; }
		[Column("amount")]
		public int Amount { get; set; }
	}
}
