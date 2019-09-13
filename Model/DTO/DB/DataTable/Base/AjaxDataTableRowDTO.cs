using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO.DB.DataTable.Base
{
	public class AjaxDataTableRowDTO
	{
		public int CurrentRecord { get; set; }
		public int TotalPage { get; set; }
		public int TotalRecord { get; set; }
	}
}
