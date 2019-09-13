using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO.DB.DataTable.Common
{
	public class AjaxDataTableCriteria
	{
		public static _SortDirection SortDirection { get; private set; } = new _SortDirection();

		public class _SortDirection
		{
			public string ASC = "ASC";
			
			public string DESC = "DESC";
		}
	}
}
