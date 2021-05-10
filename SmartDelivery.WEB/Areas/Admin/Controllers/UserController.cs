using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartDelivery.Infrastructure.Common.Pagination;
using SmartDelivery.Infrastructure.Common.StaticDetails;
using SmartDelivery.Infrastructure.Services.Interfaces;
using SmartDelivery.WEB.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartDelivery.WEB.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = StaticDetails.Admin)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        private const int PageSize = 10;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(int productPage = 1, string searchByEmail = null)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var (users, usersCount) = await _userService.GetFiltered(claim.Value, searchByEmail, PageSize, productPage);

            var userListVM = new UserListViewModel()
            {
                Users = users
            };

            const string Url = "/Admin/User/Index?productPage=:";

            userListVM.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItem = usersCount,
                UrlParam = Url
            };

            return View(userListVM);
        }

        public async Task<IActionResult> Lock(string id)
        {
            await _userService.Lock(id, true);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UnLock(string id)
        {
            await _userService.Lock(id, false);

            return RedirectToAction(nameof(Index));
        }
    }
}

