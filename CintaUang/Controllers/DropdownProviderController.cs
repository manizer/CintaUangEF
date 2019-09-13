using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CintaUang.Helpers.TagHelpers.DDLTagHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Lib.DropdownLibs;
using Repository.Repositories.CategoryRepositories;

namespace CintaUang.Controllers
{
    public class DropdownProviderController : Controller
    {
		private readonly ICategoryRepository categoryRepository;

		public DropdownProviderController(ICategoryRepository categoryRepository)
		{
			this.categoryRepository = categoryRepository;
		}
        public IActionResult ChildDropdown(string ParentId, string SubDropdownKey, int ParentValue)
        {
			List<DropdownItem> DropdownItems = SelectListItemsRegistry.Get().GetFromDict(ParentId);
			SubDropdownItemProviderFactory subDropdownItemProviderFactory = DropdownItems.Where(x => x.DropdownValue() == ParentValue.ToString()).FirstOrDefault();
			List<DropdownItem> subDropdownItems = subDropdownItemProviderFactory?.GetSubDropdownItems(SubDropdownKey) ?? new List<DropdownItem>();
			List<SelectListItem> selectListItems = Dropdown.From(subDropdownItems);
			return Json(selectListItems);
		}
    }
}