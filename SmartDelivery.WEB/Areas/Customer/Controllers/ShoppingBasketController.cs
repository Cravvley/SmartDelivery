using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartDelivery.Data.Entities;
using SmartDelivery.Infrastructure.Common.StaticDetails;
using SmartDelivery.Infrastructure.Services.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartDelivery.WEB.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingBasketController : Controller
    {
        private readonly IShoppingBasketService _shoppingBasketService;
        private readonly IRestaurantService _restaurantService;

        public ShoppingBasketController(IShoppingBasketService shoppingBasketService, IRestaurantService restaurantService)
        {
            _shoppingBasketService = shoppingBasketService;
            _restaurantService = restaurantService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShoppingCart(ShoppingCart shoppingCart)
        {
            if (shoppingCart is null)
            {
                throw new ArgumentNullException("Shopping cart doesn't exist");
            }

            await _shoppingBasketService.CreateShoppingCart(shoppingCart);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var claims = (ClaimsIdentity)User.Identity;
            Claim claim = claims.FindFirst(ClaimTypes.NameIdentifier);

            var restaurantShoppingCartId = (await _shoppingBasketService.
                                        GetShoppingCarts(p => p.UserId==claim.Value)).Select(l => l.RestaurantId).ToHashSet();

            var restaurantList =await  _restaurantService.GetAllDetails(r => restaurantShoppingCartId.Contains(r.Id));
            return View(restaurantList);
        }

        [HttpGet]
        public async Task<IActionResult> UserShoppingCartsFromRestaurant(int id)
        {
            var claims = (ClaimsIdentity)User.Identity;
            Claim claim = claims.FindFirst(ClaimTypes.NameIdentifier);
            if (claim is null)
            {
                throw new ArgumentNullException("User doesn't exist");
            }

            var returnedList = await _shoppingBasketService.GetShoppingCarts(s => s.UserId == claim.Value && s.RestaurantId== id);
            TempData[StaticDetails.RestaurantMealsId] = id;
            return View(returnedList);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteShoppingCart(int? id)
        {

            if (id is null)
            {
                throw new ArgumentNullException("Shopping cart doesn't exist");
            }

            await _shoppingBasketService.DeleteShoppingCart(id);

            var claims = (ClaimsIdentity)User.Identity;
            Claim claim = claims.FindFirst(ClaimTypes.NameIdentifier);

            var count = (await _shoppingBasketService.GetShoppingCarts(u => u.UserId == claim.Value))
                                        .GroupBy(r => r.RestaurantId).Count();

            HttpContext.Session.SetInt32(StaticDetails.ShoppingCartCount, count);

            //return RedirectToAction(nameof(Index));

            return RedirectToAction("UserShoppingCartsFromRestaurant", new { id = Convert.ToInt32(TempData[StaticDetails.RestaurantMealsId].ToString()) });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateShoppingCart(int? id, string submit)
        {
            if (id is null)
            {
                throw new ArgumentNullException("Shopping cart doesn't exist");
            }

            var shoppingCart = await _shoppingBasketService.GetShoppingCart(id);

            if (submit == "add")
            {
                shoppingCart.Count++;
            }
            else if (submit == "minus")
            {
                if (shoppingCart.Count > 1)
                {
                    shoppingCart.Count--;
                }   
            }

            await _shoppingBasketService.UpdateShoppingCart(shoppingCart);

            return RedirectToAction("UserShoppingCartsFromRestaurant", new { id=Convert.ToInt32(TempData[StaticDetails.RestaurantMealsId].ToString())});
        }
    }
}
