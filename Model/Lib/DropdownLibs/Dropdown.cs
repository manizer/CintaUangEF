using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Lib.DropdownLibs
{
	public class Dropdown
	{
		public static List<SelectListItem> From(IEnumerable<DropdownItem> dropdownItems) => dropdownItems.Select(x =>
			new SelectListItem
			{
				Text = x.DropdownText(),
				Value = x.DropdownValue()
			})
			.ToList();
	}
}
