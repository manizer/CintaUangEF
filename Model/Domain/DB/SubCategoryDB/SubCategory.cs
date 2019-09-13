using Model.Domain.DB.CategoryDB;
using Model.Lib.DropdownLibs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Domain.DB.SubCategoryDB
{
	public class SubCategory : DropdownItem
	{
		public int Id { get; set; }
		public int CategoryId { get; set; }
		public string Name { get; set; }
		

		#region Dropdown
		public bool DropdownItemEnabled() => true;
		public string DropdownValue() => Id.ToString();
		public string DropdownText() => Name.ToString();

		public List<DropdownItem> GetSubDropdownItems(string Key)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
