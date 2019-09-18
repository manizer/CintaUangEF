using Model.Domain.DataTable.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Domains.CategoryDomains
{
	public class CategoryDomain
	{
		public class Category
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}

		public class InsertCategory
		{
			public string Name { get; set; }
		}

		public class UpdateCategory
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}

		public class DeleteCategory
		{
			public int Id { get; set; }
		}
	}
}
