using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO.DB.CategoryDB
{
	public class UpdateCategoryDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int AuditedUserId { get; set; }
	}
}
