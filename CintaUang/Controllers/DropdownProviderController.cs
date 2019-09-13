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
			DropdownItem selectedDdlItem = DropdownItems.Where(x => x.DropdownValue() == ParentValue.ToString()).FirstOrDefault();

			if (selectedDdlItem.GetType() != typeof(SubDropdownItemProviderFactory))
			{
				throw new InvalidParentDropdownException($"Unable to create SubDropdownFactory from parent dropdown {ParentId}, make sure parent dropdown implements SubDropdownItemProviderFactory");
			}

			SubDropdownItemProviderFactory subDropdownItemProviderFactory = (SubDropdownItemProviderFactory)selectedDdlItem;
			List<DropdownItem> subDropdownItems = subDropdownItemProviderFactory?.GetSubDropdownItems(SubDropdownKey) ?? new List<DropdownItem>();
			List<SelectListItem> selectListItems = Dropdown.From(subDropdownItems);
			return Json(selectListItems);
		}

		public class InvalidParentDropdownException : Exception
		{
			public InvalidParentDropdownException(string ErrMsg) : base(ErrMsg) { }
		}
	}
}