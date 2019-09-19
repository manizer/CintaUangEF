using Model.DTO.DB.DataTable.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.DTO.DB.DataTable
{
	public class CategoryDataTableRowDTO : AjaxDataTableRowDTO
	{
		[Key]
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
