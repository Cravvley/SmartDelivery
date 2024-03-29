﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartDelivery.Data.Entities;
using SmartDelivery.Infrastructure.Common.StaticDetails;
using SmartDelivery.Infrastructure.Services.Interfaces;
using SmartDelivery.WEB.Models;

using System.Threading.Tasks;

namespace SmartDelivery.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Roles = StaticDetails.Admin)]
        public async Task<IActionResult> Index()
         => View(await _categoryService.GetAll(x => x.ParentId == null));

        [Authorize(Roles = StaticDetails.Admin)]
        public IActionResult Create(int? id = null)
        {
            ViewBag.Exist = false;

            var category = new Category();

            ViewBag.Title = "Category";

            if (!(id is null))
            {
                category.ParentId = id;
                ViewBag.Title = "Subcategory";
            }

            return View(category);
        }

        [HttpPost, ValidateAntiForgeryToken,Authorize(Roles = StaticDetails.Admin)]
        public async Task<IActionResult> Create(Category category)
        {
            ViewBag.Title = "Category";

            if (!(category.ParentId is null))
            {
                ViewBag.Title = "Subcategory";
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            category.Id = 0;
            await _categoryService.Create(category);

            if (category.ParentId is null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Details", new { id = category.ParentId });
            }
        }

        [Authorize(Roles = StaticDetails.Admin)]
        public async Task<IActionResult> Details(int? id)
            => View(new CategoryViewModel()
            {
                Category = await _categoryService.Get(id),
                Categories = await _categoryService.GetWithParentId(id)
            });

        [Authorize(Roles = StaticDetails.Admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            var categoryEntity = await _categoryService.Get(id);

            ViewBag.Exist = false;

            return View(categoryEntity);
        }

        [HttpPost, ValidateAntiForgeryToken,Authorize(Roles = StaticDetails.Admin)]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            await _categoryService.Update(category);

            if (category.ParentId is null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Details", new { id = category.ParentId });
            }
        }

        [Authorize(Roles = StaticDetails.Admin)]
        public async Task<IActionResult> Delete(int? id)
                => View(await _categoryService.Get(id));

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken,Authorize(Roles = StaticDetails.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parentId = (await _categoryService.Get(id)).ParentId;

            await _categoryService.Delete(id);

            if (parentId is null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Details", new { id = parentId });
            }
        }

        [Authorize]
        public async Task<IActionResult> GetCategories()
            => Json(await _categoryService.GetAllCategoryWithSubCategory());
    }
}
