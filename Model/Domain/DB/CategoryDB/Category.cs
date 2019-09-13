
using Model.Domain.DB.SubCategoryDB;
using Model.Lib.DropdownLibs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Domain.DB.CategoryDB
{
	public class Category : DropdownItem
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public Lazy<List<SubCategory>> Subcategories { get; set; }

		#region Dropdown
		public string DropdownValue() => Id.ToString();
		public string DropdownText() => Name.ToString();
		public bool DropdownItemEnabled() => false;
		public class DropdownKeys
		{
			public const string DROPDOWN_SUBCATEGORIES = "DROPDOWN_SUBCATEGORIES";
		}

		public List<DropdownItem> GetSubDropdownItems(string Key)
		{
			switch (Key)
			{
				case DropdownKeys.DROPDOWN_SUBCATEGORIES:
			var subcategories = Subcategories.Value;
					return subcategories?.ToList<DropdownItem>();
			}
			return null;
		}
		#endregion
	}
}
