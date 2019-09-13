using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CintaUang.Core.ApplicationSession;
using CintaUang.ViewModels.SubCategoryViewModels;
using CintaUang.ViewModels.SubCategoryViewModels.Components;
using Microsoft.AspNetCore.Mvc;
using Model.Domain;
using Model.Domain.DB;
using Model.Lib.DropdownLibs;
using Service.Modules;
using static Model.Domains.SubCategoryDomains.SubCategoryDomain;

namespace CintaUang.Controllers
{
	public class SubCategoryController : BaseController
	{
		private readonly ISubCategoryService subCategoryService;
		public SubCategoryController(ISubCategoryService subCategoryService)
		{
			this.subCategoryService = subCategoryService;
		}

		public IActionResult Index()
		{
			List<Category> categories = subCategoryService.GetCategories().ToList();
			return View(new IndexViewModel()
			{
				CategorySelectListItems = Dropdown.From(categories)
			});
		}

		public IActionResult Save(IndexViewModel indexViewModel)
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
					ExecuteResult insertResult = subCategoryService.InsertSubCategory(new InsertSubCategory
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
					ExecuteResult updateResult = subCategoryService.UpdateSubCategory(new UpdateSubCategory
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
		public IActionResult SubCategoryTableViewComponentOnSearch(SubCategoriesTableViewModel subCategoriesTableViewModel)
		{
			AddNotification(ViewNotification.Make("Success Search", ViewNotification.SUCCESS));
			return ViewComponent("SubCategoryTable", subCategoriesTableViewModel.CategoryId);
		}
		#endregion
	}
}