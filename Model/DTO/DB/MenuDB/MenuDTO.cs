using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.DTO.DB.MenuDB
{
	[Table("mstable")]
	public class MenuDTO
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }
		[Column("name")]
		public string Name { get; set; }
		[Column("url")]
		public string Url { get; set; }
		[Column("parentid")]
		public int ParentId { get; set; }
		[Column("position")]
		public int Position { get; set; }
		[Column("icon")]
		public string Icon { get; set; }
	}
}
