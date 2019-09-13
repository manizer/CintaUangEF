using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Model.Domains.SubCategoryDomains.SubCategoryDomain;

namespace CintaUang.ViewModels.SubCategoryViewModels.Components
{
	public class SubCategoriesTableViewModel
	{
		public List<SelectListItem> CategorySelectListItems { get; set; }
		public int CategoryId { get; set; }

		public List<SubCategory> SubCategories { get; set; }
	}
}
