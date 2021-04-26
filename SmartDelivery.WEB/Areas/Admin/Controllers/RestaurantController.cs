﻿using Microsoft.AspNetCore.Mvc;
using SmartDelivery.Data.Entities;
using SmartDelivery.Infrastructure.Common.Pagination;
using SmartDelivery.Infrastructure.Services.Interfaces;
using SmartDelivery.WEB.Models;
using System.Threading.Tasks;

namespace SmartDelivery.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        
        private const int PageSize = 5;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        public async Task<IActionResult> Index(int productPage = 1, string searchByRestaurant = null, string searchByCity = null)
        {
            var (restaurants, restaurantsCount) = await _restaurantService.GetFiltered(searchByRestaurant, searchByCity, PageSize, productPage);

            var restaurantListVM = new RestaurantListViewModel()
            {
                Restaurants= restaurants
            };

            const string Url = "/Admin/Restaurant/Index?productPage=:";

            restaurantListVM.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItem = restaurantsCount,
                UrlParam = Url
            };

            return View(restaurantListVM);
        }

        public IActionResult Create()
        {
            ViewBag.Exist = false;
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return View(restaurant);
            }

            var exist = await _restaurantService.Exist(s => s.Address.City.ToLower() == restaurant.Address.City.ToLower() && s.Address.Country.ToLower()
                                == restaurant.Address.Country.ToLower() && s.Name.ToLower() == restaurant.Name.ToLower() && s.Address.ZipCode.ToLower()
                                == restaurant.Address.ZipCode.ToLower() && s.Address.FlatNumber.ToLower() == restaurant.Address.FlatNumber.ToLower() 
                                && s.Address.HouseNumber.ToLower() == restaurant.Address.HouseNumber.ToLower());

            if (exist)
            {
                ViewBag.Exist = true;

                return View(restaurant);
            }

            await _restaurantService.Create(restaurant);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Exist = false;

            return View(await _restaurantService.Get(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return View(restaurant);
            }

            var exist = await _restaurantService.Exist(s => s.Address.City.ToLower() == restaurant.Address.City.ToLower() && s.Address.Country.ToLower()
                               == restaurant.Address.Country.ToLower() && s.Name.ToLower() == restaurant.Name.ToLower() && s.Address.ZipCode.ToLower()
                               == restaurant.Address.ZipCode.ToLower() && s.Address.FlatNumber.ToLower() == restaurant.Address.FlatNumber.ToLower()
                               && s.Address.HouseNumber.ToLower() == restaurant.Address.HouseNumber.ToLower());
            if (exist)
            {
                ViewBag.Exist = true;

                return View(restaurant);
            }

            await _restaurantService.Update(restaurant);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
                     => View(await _restaurantService.Get(id));

        public async Task<IActionResult> Delete(int? id)
                => View(await _restaurantService.Get(id));

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _restaurantService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

    }
}