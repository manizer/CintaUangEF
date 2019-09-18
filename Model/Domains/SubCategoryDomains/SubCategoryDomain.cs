using Model.Lib.DropdownLibs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Domains.SubCategoryDomains
{
	public class SubCategoryDomain
	{
		public class Category : DropdownItem
		{
			public int Id { get; set; }
			public string Name { get; set; }

			public bool DropdownItemEnabled() => true;

			public string DropdownText() => Name;

			public string DropdownValue() => Id.ToString();
		}

		public class SubCategory
		{
			public int Id { get; set; }
			public int CategoryId { get; set; }
			public string Name { get; set; }
			public Lazy<Category> Category { get; set; }
		}

		public class InsertSubCategory
		{
			public int CategoryId { get; set; }
			public string SubCategoryName { get; set; }
			public int AuditedUserId { get; set; }
		}

		public class UpdateSubCategory
		{
			public int SubCategoryId { get; set; }
			public int CategoryId { get; set; }
			public string SubCategoryName { get; set; }
			public int AuditedUserId { get; set; }
		}
	}
}
