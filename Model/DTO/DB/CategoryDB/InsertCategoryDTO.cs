using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO.DB.CategoryDB
{
	public class InsertCategoryDTO
	{
		public string Name { get; set; }
		public int AuditedUserId { get; set; }
	}
}
