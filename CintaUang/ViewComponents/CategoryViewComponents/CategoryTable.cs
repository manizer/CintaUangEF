using CintaUang.Helpers.ViewComponentHelpers;
using CintaUang.ViewModels.CategoryViewModels;
using CintaUang.ViewModels.CategoryViewModels.Components;
using Microsoft.AspNetCore.Mvc;
using Model.Domain.DB;
using Model.Domain.DB.CategoryDB;
using Model.DTO.DB;
using Repository.Base.Helper;
using Repository.Repositories.CategoryRepositories;
using Service.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CintaUang.ViewComponents.CategoryViewComponents
{
	public class CategoryTable : ViewComponent
	{
		private readonly ICategoryService categoryService;

		public CategoryTable(ICategoryService categoryService)
		{
			this.categoryService = categoryService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			List<Category> Categories = (await categoryService.GetCategories())?.ToList();
			return View(ViewComponentPath.ViewPath("Category", "_CategoryTable"), new CategoriesTableViewModel
			{
				Categories = Categories
			});
		}
	}
}
