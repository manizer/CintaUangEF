using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CintaUang.Core.ApplicationSession;
using CintaUang.ViewComponents.SubCategoryViewComponents;
using CintaUang.ViewModels.SubCategoryViewModels;
using CintaUang.ViewModels.SubCategoryViewModels.Components;
using Microsoft.AspNetCore.Mvc;
using Model.Domain;
using Model.Domain.DB;
using Model.Domain.DB.CategoryDB;
using Model.Domain.DB.SubCategoryDB;
using Model.Lib.DropdownLibs;
using Service.Modules;

namespace CintaUang.Controllers
{
	public class SubCategoryController : BaseController
	{
		private readonly ISubCategoryService subCategoryService;
		public SubCategoryController(ISubCategoryService subCategoryService)
		{
			this.subCategoryService = subCategoryService;
		}

		public async Task<IActionResult> Index()
		{
			List<Category> categories = (await subCategoryService.GetCategories()).ToList();
			return View(new IndexViewModel()
			{
				CategorySelectListItems = Dropdown.From(categories)
			});
		}

		public async Task<IActionResult> Save(IndexViewModel indexViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View("Index", indexViewModel);
			}

			try
			{
				if (indexViewModel.SubCategoryId == 0)
				{
					// Insert
					ExecuteResult insertResult = await subCategoryService.InsertSubCategory(new Model.Domain.DB.SubCategoryDB.InsertSubCategory
					{
						CategoryId = indexViewModel.CategoryId,
						SubCategoryName = indexViewModel.SubCategoryName,
						AuditedUserId = HttpContext.Session.GetLoginUserId() ?? 0
					});
					AddNotification(ViewNotification.Make("Insert Success", "Success"));
				}
				else
				{
					// Update
					ExecuteResult updateResult = await subCategoryService.UpdateSubCategory(new Model.Domain.DB.SubCategoryDB.UpdateSubCategory
					{
						SubCategoryId = indexViewModel.SubCategoryId,
						CategoryId = indexViewModel.CategoryId,
						SubCategoryName = indexViewModel.SubCategoryName,
						AuditedUserId = HttpContext.Session.GetLoginUserId() ?? 0
					});
					AddNotification(ViewNotification.Make("Update Success", ViewNotification.SUCCESS));
				}
			}
			catch (Exception e)
			{
				AddNotification(ViewNotification.Make("Error", ViewNotification.ERROR));
				return View("Index", indexViewModel);
			}

			return RedirectToAction("Index", "SubCategory");
		}

		#region View Component
		[HttpPost]
		public async Task<IActionResult> SubCategoryTableViewComponentOnSearch(SubCategoriesTableViewModel subCategoriesTableViewModel)
		{
			AddNotification(ViewNotification.Make("Success Search", ViewNotification.SUCCESS));
			return ViewComponent("SubCategoryTable", subCategoriesTableViewModel.CategoryId);
		}
		#endregion
	}
}