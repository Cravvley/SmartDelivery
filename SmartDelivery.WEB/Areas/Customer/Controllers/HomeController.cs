using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartDelivery.Data.Entities;
using SmartDelivery.Infrastructure.Common.Pagination;
using SmartDelivery.Infrastructure.Common.StaticDetails;
using SmartDelivery.Infrastructure.Services.Interfaces;
using SmartDelivery.WEB.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartDelivery.WEB.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IDishService _dishService;
        private readonly IRestaurantService _restaurantService;
        private readonly ICategoryService _categoryService;

        private const int PageSize = 9;
        public HomeController(IDishService productService, IRestaurantService shopService, ICategoryService categoryService)
        {
            _dishService = productService;
            _restaurantService = shopService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int productPage = 1)
        {
            var restaurants = await _restaurantService.GetAllDetails();
            var homeVM = new HomeViewModel()
            {
                Restaurants = restaurants.OrderBy(p => p.Id).Skip((productPage - 1) *
                 PageSize).Take(PageSize).ToList(),
            };

            const string Url = "/Customer/Home/Index?productPage=:";

            homeVM.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItem = restaurants.Count,
                UrlParam = Url
            };

            return View(homeVM);
        }

        public async Task<IActionResult> Meals(int productPage, int? id,string searchByCategory)
        {

            if (!(id is null))
            {
                TempData[StaticDetails.RestaurantMealsId] = id;
            }

            var meals = await _restaurantService.GetMealsByRestaurant(Convert.ToInt32(TempData[StaticDetails.RestaurantMealsId].ToString()));

            foreach(Dish dish in meals)
            {
                dish.Category = await _categoryService.Get(dish.CategoryId);
            }

            if (searchByCategory is null)
            {
                meals = meals.OrderBy(p => p.Category.Title).Skip((productPage - 1) * PageSize).Take(PageSize).ToList();
            }
            else
            {
                meals = meals.OrderBy(p => p.Category.Title).Where(p => p.Category.Title.ToLowerInvariant().
                                Contains(searchByCategory.ToLowerInvariant()))
                                .Skip((productPage - 1) * PageSize).Take(PageSize).ToList();
            }

            var dishList = new DishViewModel()
            {
                Products = meals.OrderBy(p => p.Title).Skip((productPage - 1) *
                PageSize).Take(PageSize).ToList(),
            };

            string Url = $"/Customer/Home/Meals/{TempData[StaticDetails.RestaurantMealsId]}?productPage=:";

            dishList.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItem = meals.Count,
                UrlParam = Url
            };

            return View(dishList);
        }

        [Authorize]
        public async Task<IActionResult> DishDetails(int id)
        {
            var menuItemFromDb = await _dishService.Get(id);

            ShoppingCart cartObj = new ShoppingCart()
            {
                Dish = menuItemFromDb,
                DishId = menuItemFromDb.Id
            };
            return View(cartObj);
        }

        /*if
       [Authorize]
       [HttpPost]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> DishDetails(ShoppingCart CartObject)
       {
           CartObject.Dish = null;
           CartObject.Id = 0;

           (ModelState.IsValid)
           {
               var claimsIdentity = (ClaimsIdentity)this.User.Identity;
               var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
               CartObject.UserId = Convert.ToInt32(claim.Value);

               ShoppingCart shoppingCart = await _shoppingBasketService.GetProduct(c => c.UserId == CartObject.UserId
                                            && c.DishId == CartObject.DishId);

               if (shoppingCart is null)
               {
                   await _shoppingBasketService.CreateProduct(CartObject);
               }
               else
               {
                   shoppingCart.Count = shoppingCart.Count + CartObject.Count;
                   await _shoppingBasketService.UpdateProduct(shoppingCart);

               }

               var count = (await _shoppingBasketService.GetProducts(CartObject.UserId)).Count;

               HttpContext.Session.SetInt32(SD.ShoppingCartCount, count);

               return RedirectToAction("Index");
           }
           else
           {

               var menuItemFromDb = await _dishService.Get(CartObject.DishId);

               ShoppingCart cartObj = new ShoppingCart()
               {
                   Dish = menuItemFromDb,
                   DishId = menuItemFromDb.Id
               };

               return View(cartObj);
           }
       }
           */
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
