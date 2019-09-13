using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.DTO.DB.SubCategoryDB
{
	public class SubCategoryDTO
	{
		[Key]
		public int Id { get; set; }
		public int CategoryId { get; set; }
		public string Name { get; set; }
	}
}
