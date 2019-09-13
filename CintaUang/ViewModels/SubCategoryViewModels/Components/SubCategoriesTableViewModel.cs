using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Domain.DB.CategoryDB;
using Model.Domain.DB.SubCategoryDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CintaUang.ViewModels.SubCategoryViewModels.Components
{
	public class SubCategoriesTableViewModel
	{
		public List<SelectListItem> CategorySelectListItems { get; set; }
		public int CategoryId { get; set; }

		public List<SubCategory> SubCategories { get; set; }
	}
}
