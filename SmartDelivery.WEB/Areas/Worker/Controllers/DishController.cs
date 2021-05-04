using Microsoft.AspNetCore.Mvc;
using SmartDelivery.Data.Entities;
using SmartDelivery.Infrastructure.Common.Pagination;
using SmartDelivery.Infrastructure.Services.Interfaces;
using SmartDelivery.WEB.Models;
using System.IO;
using System.Threading.Tasks;

namespace SmartDelivery.WEB.Areas.Worker.Controllers
{
    [Area("Worker")]
    public class DishController : Controller
    {
        private readonly IDishService _dishService;
        private readonly ICategoryService _categoryService;

        private const int PageSize = 5;
        public DishController(IDishService dishService,
            ICategoryService categoryService)
        {
            _dishService = dishService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int productPage = 1, string productName = null, string categoryName = null)
        {
            var (products, productsCount) = await _dishService.GetFiltered(productName, categoryName, PageSize, productPage);

            var productList = new DishViewModel()
            {
                Products = products,
            };

            const string Url = "/Dish/Index?productPage=:";

            productList.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItem = productsCount,
                UrlParam = Url
            };

            return View(productList);
        }

        [HttpGet]
        public IActionResult Create()
            => View(new Dish());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dish dishModel)
        {
            if (!ModelState.IsValid) return View(dishModel);

            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                byte[] p1 = null;
                using (var fs1 = files[0].OpenReadStream())
                {
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                }
                dishModel.Image = p1;
            }

            await _dishService.Create(dishModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
            => View(await _dishService.Get(id));

        public async Task<IActionResult> Edit(int? id)
        {
            var dish = await _dishService.Get(id);

            ViewBag.Exist = false;

            return View(dish);
        }

        [HttpPost, ActionName("Edit"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPOST(Dish dish)
        {
            if (!ModelState.IsValid)
            {
                return View(dish);
            }

            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                byte[] p1 = null;
                using (var fs1 = files[0].OpenReadStream())
                {
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                }
                dish.Image = p1;
            }

            await _dishService.Update(dish);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
            => View(await _dishService.Get(id));

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int id)
        {
            await _dishService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
