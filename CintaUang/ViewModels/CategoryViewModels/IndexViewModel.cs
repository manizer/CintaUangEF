using Model.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CintaUang.ViewModels.CategoryViewModels
{
	public class IndexViewModel
	{
		#region Form Data
		public int CategoryId { get; set; }

		[Required(ErrorMessage = "Category Name is Required")]
		public string CategoryName { get; set; }
		#endregion
	}
}
