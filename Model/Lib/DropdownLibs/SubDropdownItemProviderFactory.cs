using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Lib.DropdownLibs
{
	public interface SubDropdownItemProviderFactory
	{
		List<DropdownItem> GetSubDropdownItems(string Key);
	}
}
