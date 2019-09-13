using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CintaUang.ViewModels.SubCategoryViewModels
{
    public class IndexViewModel
    {
		#region Form Subcategory
		public List<SelectListItem> CategorySelectListItems { get; set; }
		public int SubCategoryId { get; set; }
		[Required]
		public int CategoryId { get; set; }
		[Required]
		public string SubCategoryName { get; set; }
		#endregion
    }
}
