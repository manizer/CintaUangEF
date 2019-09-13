using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CintaUang.Core.ApplicationSession;
using CintaUang.ViewModels.CategoryViewModels;
using Microsoft.AspNetCore.Mvc;
using Model.Domain;
using Model.Domain.DB;
using Model.Domain.DB.DataTable;
using Model.DTO.DB.DataTable.Common;
using Service.Modules;

using static Model.Domains.CategoryDomains.CategoryDomain;

namespace CintaUang.Controllers.CategoryControllers
{
    public class CategoryController : BaseController
    {
		private readonly ICategoryService categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			this.categoryService = categoryService;
		}

		public IActionResult Index()
		{
			return View(new IndexViewModel());
		}

		public IActionResult Save(IndexViewModel indexViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View("Index", indexViewModel);
			}

			try
			{
				if (indexViewModel.CategoryId == 0)
				{
					// Insert
					ExecuteResult insertResult = categoryService.Insert(new InsertCategory
					{
						Name = indexViewModel.CategoryName,
						AuditedUserId = HttpContext.Session.GetLoginUserId() ?? 0
					});
					AddNotification(ViewNotification.Make("Insert Success", "Success"));
				}
				else
				{
					// Update
					ExecuteResult updateResult = categoryService.Update(new UpdateCategory
					{
						Id = indexViewModel.CategoryId,
						Name = indexViewModel.CategoryName,
						AuditedUserId = HttpContext.Session.GetLoginUserId() ?? 0
					});
					AddNotification(ViewNotification.Make("Update Success", ViewNotification.SUCCESS));
				}
			}
			catch(Exception e)
			{
				AddNotification(ViewNotification.Make("Error", ViewNotification.ERROR));
				return View("Index", indexViewModel);	
			}

			return RedirectToAction("Index", "Category");
		}

		public async Task<JsonResult> DTT(int draw, int start, int length)
		{
			try
			{
				int Page = (start / length) + 1;
				AjaxDataTable<CategoryDataTableRow> categoryAjaxDataTable = await categoryService.GetCategoryDataTable(Page, length , "", 0, AjaxDataTableCriteria.SortDirection.ASC);
				categoryAjaxDataTable.Draw = draw;
				return Json(categoryAjaxDataTable);
			}
			catch(Exception e)
			{
				throw e;
			}
		}
	}
}