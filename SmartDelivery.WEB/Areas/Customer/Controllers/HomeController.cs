using Microsoft.AspNetCore.Mvc;
using SmartDelivery.Infrastructure.Common.Pagination;
using SmartDelivery.Infrastructure.Services.Interfaces;
using SmartDelivery.WEB.Models;
using System.Diagnostics;
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
            // var (products, productsCount) = await _dishService.GetFiltered(searchByProduct, searchByCategory, searchByShop, PageSize, productPage);

            var restaurants = await _restaurantService.GetAllDetails();
            var homeVM = new HomeViewModel()
            {
                Restaurants= restaurants,
            };

            const string Url = "/User/Home/Index?productPage=:";

            homeVM.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItem = 10,
                UrlParam = Url
            };

            return View(homeVM);
        }

        public async Task<IActionResult> Meals(int? id)
        {
            //var (products, productsCount) = await _dishService.GetFiltered(productName, categoryName, PageSize, productPage);

            var dishList = new DishViewModel()
            {
                Products = await _restaurantService.GetMealsByRestaurant(id)
            };

            const string Url = "/Home/Meals?productPage=:";

            dishList.PagingInfo = new PagingInfo
            {
                CurrentPage = 1,
                ItemsPerPage = PageSize,
                TotalItem = 10,
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
