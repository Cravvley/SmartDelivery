using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IShoppingBasketService _shoppingBasketService;

        private const int PageSize = 8;
        public HomeController(IDishService productService, IRestaurantService shopService,
            ICategoryService categoryService, IShoppingBasketService shoppingBasketService)
        {
            _dishService = productService;
            _restaurantService = shopService;
            _categoryService = categoryService;
            _shoppingBasketService = shoppingBasketService;
        }

        public async Task<IActionResult> Index(int productPage = 1, string searchByRestaurant = null, string searchByCity = null)
        {
   
            var (restaurants, restaurantsCount) = await _restaurantService.GetFiltered(searchByRestaurant, searchByCity, PageSize, productPage);
            var homeVM = new HomeViewModel()
            {
                Restaurants = restaurants
            };

            const string Url = "/Customer/Home/Index?productPage=:";

            homeVM.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItem = restaurantsCount,
                UrlParam = Url
            };

            return View(homeVM);
        }

        public async Task<IActionResult> Meals(int productPage, int? id,string searchByCategory)
        {

            var meals = await _restaurantService.GetMealsByRestaurant(id);

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
                Categories = await _categoryService.GetAll()
            };

            string Url = $"/Customer/Home/Meals/{TempData[StaticDetails.RestaurantMealsId]}?productPage=:";

            dishList.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItem = meals.Count,
                UrlParam = Url
            };

            TempData[StaticDetails.RestaurantMealsId] = id;

            return View(dishList);
        }

        [Authorize]
        public async Task<IActionResult> DishDetails(int id)
        {
            var menuItemFromDb = await _dishService.Get(id);
            var restaurantId = Convert.ToInt32(TempData[StaticDetails.RestaurantMealsId].ToString());

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCart cartObj = new ShoppingCart()
            {
                Dish = menuItemFromDb,
                DishId = menuItemFromDb.Id,
                RestaurantId = restaurantId,
                UserId = claim.Value
        };

            return View(cartObj);
        }

        [ValidateAntiForgeryToken, Authorize, HttpPost]
        public async Task<IActionResult> DishDetails(ShoppingCart CartObject)
        {
            CartObject.Dish = null;
            CartObject.Id = 0;
   
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                CartObject.UserId = claim.Value;

                ShoppingCart shoppingCart = await _shoppingBasketService.GetShoppingCart(c => c.UserId == CartObject.UserId
                                             && c.DishId == CartObject.DishId&&c.RestaurantId==CartObject.RestaurantId);

                if (shoppingCart is null)
                {
                    await _shoppingBasketService.CreateShoppingCart(CartObject);
                }
                else
                {
                    shoppingCart.Count = shoppingCart.Count + CartObject.Count;
                    await _shoppingBasketService.UpdateShoppingCart(shoppingCart);
                }

                var count = (await _shoppingBasketService.GetShoppingCarts(u => u.UserId == CartObject.UserId))
                                        .GroupBy(r => r.RestaurantId).Count();

                HttpContext.Session.SetInt32(StaticDetails.ShoppingCartCount, count);

                return RedirectToAction("Meals",new { id = CartObject.RestaurantId });
            }
            else
            {
                var menuItemFromDb = await _dishService.Get(CartObject.DishId);

                ShoppingCart cartObj = new ShoppingCart()
                {
                    Dish = menuItemFromDb,
                    DishId = menuItemFromDb.Id,
                    RestaurantId= CartObject.RestaurantId,
                };

                return View(cartObj);
            }
        }
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
