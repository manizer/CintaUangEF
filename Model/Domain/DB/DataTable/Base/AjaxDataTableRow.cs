using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Domain.DataTable.Base
{
	public class AjaxDataTableRow
	{
		[JsonProperty("currentRecord")]
		public int CurrentRecord { get; set; }
		[JsonProperty("totalPage")]
		public int TotalPage { get; set; }
		[JsonProperty("totalRecord")]
		public int TotalRecord { get; set; }
	}
}
