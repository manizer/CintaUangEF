using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CintaUang.Helpers.ViewComponentHelpers
{
	public class ViewComponentPath
	{
		public static string ViewPath(string ControllerName, string ViewName) => $"~/Views/{ControllerName}/Components/{ViewName}.cshtml";
	}
}
