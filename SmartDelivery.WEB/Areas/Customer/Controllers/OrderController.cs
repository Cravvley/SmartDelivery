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

       /* public async Task<IActionResult> Index()
            => View(await _orderService.GetAll(Convert.ToInt32(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value)));
       */
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

            var id = await _orderService.Create(order, claim.Value,1);
            await _shoppingBasketService.ClearUserShoppingCarts(claim.Value,1);

            HttpContext.Session.Remove(StaticDetails.ShoppingCartCount);

            return RedirectToAction("Index", "Payment", new { Id = id });
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderInfo(int id)
            => PartialView("PartialView/_OrderInfo", await _orderService.GetOne(id));

        [HttpGet]
        public async Task<IActionResult> CancelOrder(int id)
        {
            await _orderService.CancelOrder(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
