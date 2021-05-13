using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartDelivery.Data.Repositories;
using SmartDelivery.Infrastructure.Common.StaticDetails;
using SmartDelivery.Infrastructure.Models;
using SmartDelivery.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartDelivery.WEB.Areas.Customer.Controllers
{
    [Area("Customer"), Authorize]
    public class PaymentController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IShoppingBasketService _shoppingBasketService;

        private static readonly Random _random = new Random();
        private static List<string> FakeBlikCodes = new List<string>
        {
            $"{_random.Next(10)}{_random.Next(10)}{_random.Next(10)}{_random.Next(10)}{_random.Next(10)}{_random.Next(10)}",
            $"{_random.Next(10)}{_random.Next(10)}{_random.Next(10)}{_random.Next(10)}{_random.Next(10)}{_random.Next(10)}",
            $"{_random.Next(10)}{_random.Next(10)}{_random.Next(10)}{_random.Next(10)}{_random.Next(10)}{_random.Next(10)}",
        };

        public PaymentController( IOrderService orderService
            , IShoppingBasketService shoppingBasketService)
        {
            _orderService = orderService;
            _shoppingBasketService = shoppingBasketService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var claims = (ClaimsIdentity)User.Identity;
            var claim = claims.FindFirst(ClaimTypes.NameIdentifier);

            var restaurantShoppingCartCount = (await _shoppingBasketService.
                                       GetShoppingCarts(p => p.UserId == claim.Value)).
                                       Select(l => l.RestaurantId).ToHashSet().Count;

            HttpContext.Session.SetInt32(StaticDetails.ShoppingCartCount, restaurantShoppingCartCount);

            ViewBag.FakeBliks = new List<string>(FakeBlikCodes);
            return View(await _orderService.GetOrder(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(PayRequest payRequest)
        {

            await _orderService.DeleteOrder(payRequest.Id);

            if (!FakeBlikCodes.Contains(payRequest.BlikCode))
            {
                return View("Failed");
            }

            return View("Success");
        }
    }
}
