using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CintaUang.ViewModels.CategoryViewModels;
using Helper.Session;
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
						Name = indexViewModel.CategoryName
					});
					AddNotification(ViewNotification.Make("Insert Success", "Success"));
				}
				else
				{
					// Update
					ExecuteResult updateResult = categoryService.Update(new UpdateCategory
					{
						Id = indexViewModel.CategoryId,
						Name = indexViewModel.CategoryName
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
	}
}