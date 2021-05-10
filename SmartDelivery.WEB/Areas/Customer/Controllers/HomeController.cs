using Microsoft.AspNetCore.Mvc;
using SmartDelivery.Infrastructure.Common.Pagination;
using SmartDelivery.Infrastructure.Common.StaticDetails;
using SmartDelivery.Infrastructure.Services.Interfaces;
using SmartDelivery.WEB.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDelivery.WEB.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IDishService _dishService;
        private readonly IRestaurantService _restaurantService;

        private const int PageSize = 9;
        public HomeController(IDishService productService, IRestaurantService shopService)
        {
            _dishService = productService;
            _restaurantService = shopService;
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

        public async Task<IActionResult> Meals(int productPage, int? id)
        {

            if (!(id is null))
            {
                TempData[StaticDetails.RestaurantMealsId] = id;
            }

            var meals = await _restaurantService.GetMealsByRestaurant(Convert.ToInt32(TempData[StaticDetails.RestaurantMealsId].ToString()));
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
