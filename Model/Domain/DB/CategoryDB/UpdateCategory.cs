using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Domain.DB.CategoryDB
{
	public class UpdateCategory
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int AuditedUserId { get; set; }
	}
}
