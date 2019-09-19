using Model.Domain.DataTable.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Domain.DB.DataTable
{
	public class CategoryDataTableRow : AjaxDataTableRow
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
