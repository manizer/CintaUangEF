using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO.DB.MenuDB
{
	public sealed class MenuDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public int ParentId { get; set; }
		public int Position { get; set; }
		public string Icon { get; set; }
	}
}
