using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Lib.DropdownLibs
{
	public interface DropdownItem : SubDropdownItemProviderFactory
	{
		string DropdownValue();
		string DropdownText();
		bool DropdownItemEnabled();
	}
}
