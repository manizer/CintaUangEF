using Model.Lib.DropdownLibs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CintaUang.Helpers.TagHelpers.DDLTagHelper
{
	public class SelectListItemsRegistry
	{
		public static SelectListItemsRegistry Registry { get; set; }
		public Dictionary<string, List<DropdownItem>> DropdownItemsDict { get; private set; } = new Dictionary<string, List<DropdownItem>>();

		public static SelectListItemsRegistry Get()
		{
			if (Registry == null) Registry = new SelectListItemsRegistry();
			return Registry;
		}

		public void AddToDict(string providerDdlId, List<DropdownItem> Data)
		{
			if (DropdownItemsDict.ContainsKey(providerDdlId))
			{
				DropdownItemsDict.Remove(providerDdlId);
			}
			DropdownItemsDict.Add(providerDdlId, Data);
		}

		public List<DropdownItem> GetFromDict(string providerDdlId)
		{
			List<DropdownItem> dropdowns = new List<DropdownItem>();
			DropdownItemsDict.TryGetValue(providerDdlId, out dropdowns);
			return dropdowns;
		}
	}
}
