using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartDelivery.Data.Entities;
using SmartDelivery.Infrastructure.Common.StaticDetails;
using SmartDelivery.Infrastructure.Services.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartDelivery.WEB.Areas.Customer.Controllers
{
    [Area("Customer"),Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IShoppingBasketService _shoppingBasketService;

        public OrderController(IOrderService orderService, IShoppingBasketService shoppingBasketService)
        {
            _orderService = orderService;
            _shoppingBasketService = shoppingBasketService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            var claims = (ClaimsIdentity)User.Identity;
            var claim = claims.FindFirst(ClaimTypes.NameIdentifier);
            if (claim is null)
            {
                throw new ArgumentNullException("User doesn't exist");
            }

            var restaurantId = Convert.ToInt32(TempData[StaticDetails.RestaurantMealsId].ToString());

            var id = await _orderService.CreateOrder(order, claim.Value, restaurantId);
            await _shoppingBasketService.ClearUserShoppingCarts(claim.Value, restaurantId);

            return RedirectToAction("Index", "Payment", new { Id = id });
        }
    }
}
