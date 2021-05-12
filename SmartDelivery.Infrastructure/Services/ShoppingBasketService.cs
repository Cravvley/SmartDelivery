using AutoMapper;
using SmartDelivery.Data.Entities;
using SmartDelivery.Data.Repositories;
using SmartDelivery.Infrastructure.Repositories;
using SmartDelivery.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartDelivery.Infrastructure.Services
{
    public class ShoppingBasketService : IShoppingBasketService
    {
        private readonly IShoppingBasketRepository _shoppingBasketRepository;
        private readonly IUserService _userService;
        private readonly IRestaurantService _restaurantService;

        public ShoppingBasketService(IShoppingBasketRepository shoppingBasketRepository
            , IUserService userService, IRestaurantService restaurantService)
        {
            _shoppingBasketRepository = shoppingBasketRepository;
            _userService = userService;
            _restaurantService = restaurantService;
        }

        public async Task CreateShoppingCart(ShoppingCart shoppingCart)
        {
            if (shoppingCart is null)
            {
                throw new ArgumentNullException("Shopping cart doesn't exist");
            }

            await _shoppingBasketRepository.AddShoppingCart(shoppingCart);
        }

        public async Task<List<ShoppingCart>> GetShoppingCarts(string userId,int ?restaurantId)
        {
            var userEntity = _userService.Get(userId);

            if (userEntity is null)
            {
                throw new ArgumentNullException("User doesn't exist");
            }

            var restaurantEntity = _restaurantService.Get(restaurantId);

            if (restaurantEntity is null)
            {
                throw new ArgumentNullException("Restaurant doesn't exist");
            }

            return await _shoppingBasketRepository.GetShoppingCarts(userId,restaurantId.Value);
        }

        public async Task<ShoppingCart> GetShoppingCart(int ? productId)
        {
            var shoppingCartEntity=await _shoppingBasketRepository.GetShoppingCart(productId.Value);
            if(shoppingCartEntity is null)
            {
                throw new ArgumentNullException("Shopping cart doesn't exist");
            }

            return shoppingCartEntity;
        }

        public async Task DeleteShoppingCart(int? id)
        {
            var shoppingCartEntity = await _shoppingBasketRepository.GetShoppingCart(id.Value);

            if (shoppingCartEntity is null)
            {
                return;
            }

            await _shoppingBasketRepository.DeleteShoppingCart(shoppingCartEntity);
        }

        public async Task UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            if (shoppingCart is null)
            {
                throw new ArgumentNullException("Shopping cart doesn't exist");
            }

            await _shoppingBasketRepository.UpdateShoppingCart(shoppingCart);
        }

        public async Task ClearUserShoppingCarts(string userId,int? restaurantId)
        {
            var userEntity = _userService.Get(userId);

            if(userEntity is null)
            {
                throw new ArgumentNullException("User doesn't exist");
            }

            var restaurantEntity= _restaurantService.Get(restaurantId);

            if (restaurantEntity is null)
            {
                throw new ArgumentNullException("Restaurant doesn't exist");
            }

            var shoppingCarts = await _shoppingBasketRepository.GetShoppingCarts(userId,restaurantId.Value);

            await _shoppingBasketRepository.DeleteShoppingCartRange(shoppingCarts);
        }

        public async Task<ShoppingCart> GetShoppingCart(Expression<Func<ShoppingCart, bool>> filter)
            => await _shoppingBasketRepository.GetShoppingCart(filter);
    }
}
