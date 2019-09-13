using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.DTO.DB.SubCategoryDB
{
	[Table("mssubcategory")]
	public class SubCategoryDTO : BaseModel
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Column("categoryid")]
		public int CategoryId { get; set; }
		[Column("name")]
		public string Name { get; set; }
	}
}
