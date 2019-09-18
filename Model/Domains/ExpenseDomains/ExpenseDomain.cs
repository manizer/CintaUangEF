using Model.Domain.DataTable.Base;
using Model.Lib.DropdownLibs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Domains.ExpenseDomains
{
	public class ExpenseDomain
	{
		public class Category : DropdownItem, SubDropdownItemProviderFactory
		{
			public int Id { get; set; }
			public string Name { get; set; }

			public bool DropdownItemEnabled() => true;
			public string DropdownText() => Name?.ToString();
			public string DropdownValue() => Id.ToString();
			public Lazy<List<SubCategory>> SubCategories { get; set; }

			public class DropdownKeys
			{
				public const string DROPDOWN_SUBCATEGORIES = "DROPDOWN_SUBCATEGORIES";
			}
			public List<DropdownItem> GetSubDropdownItems(string Key)
			{
				switch (Key)
				{
					case DropdownKeys.DROPDOWN_SUBCATEGORIES:
						return SubCategories.Value.ToList<DropdownItem>();
				}
				return new List<DropdownItem>();
			}
		}

		public class SubCategory : DropdownItem
		{
			public int Id { get; set; }
			public int CategoryId { get; set; }
			public string Name { get; set; }

			public bool DropdownItemEnabled() => true;
			public string DropdownText() => Name.ToString();
			public string DropdownValue() => Id.ToString();
		}

		public class Expense
		{
			public int Id { get; set; }
			public int SubcategoryId { get; set; }
			public string Name { get; set; }
			public int Amount { get; set; }
		}

		public class ExpenseDataTableRow : AjaxDataTableRow
		{
			[JsonProperty("expenseId")]
			public int ExpenseId { get; set; }
			[JsonProperty("expenseName")]
			public string ExpenseName { get; set; }
			[JsonProperty("amount")]
			public int Amount { get; set; }
			[JsonProperty("subCategoryName")]
			public string SubCategoryName { get; set; }
			[JsonProperty("categoryName")]
			public string CategoryName { get; set; }
		}
	}
}
