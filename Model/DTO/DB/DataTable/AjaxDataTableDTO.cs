using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO.DB.DataTable
{
	public class AjaxDataTableDTO<E> where E : class
	{
		public int? Draw { get; set; }
		public int RecordsTotal { get; set; }
		public int? RecordsFiltered { get; set; }
		public List<E> Data { get; set; }
	}
}
