using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Domains.LayoutDomains
{
	public class LayoutDomain
	{
		public class Menu
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public string Url { get; set; }
			public int ParentId { get; set; }
			public int Position { get; set; }
			public string Icon { get; set; }

			List<Menu> ChildMenus { get; set; }
		}
	}
}
