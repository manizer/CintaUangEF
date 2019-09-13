using CintaUang.Helpers.ViewComponentHelpers;
using CintaUang.ViewModels.SubCategoryViewModels.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Domain.DB.CategoryDB;
using Model.Lib.DropdownLibs;
using Service.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CintaUang.ViewComponents.SubCategoryViewComponents
{
	public class SubCategoryTable : ViewComponent
	{
		private readonly ISubCategoryService subCategoryService;

		public SubCategoryTable(ISubCategoryService subCategoryService)
		{
			this.subCategoryService = subCategoryService;
		}

		public async Task<IViewComponentResult> InvokeAsync(int CategoryId)
		{
			List<Category> categories = (await subCategoryService.GetCategories()).ToList();
			return View(ViewComponentPath.ViewPath("SubCategory", "_SubCategoryTable"), new SubCategoriesTableViewModel
			{
				CategorySelectListItems = Dropdown.From(categories),
				CategoryId = CategoryId,
				SubCategories = (await subCategoryService.GetSubcategoriesByCategoryID(CategoryId)).ToList()
			});
		}
	}
}
