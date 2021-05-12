using SmartDelivery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartDelivery.Infrastructure.Services.Interfaces
{
    public interface IShoppingBasketService
    {
        Task CreateShoppingCart(ShoppingCart shoppingCart);

        Task<List<ShoppingCart>> GetShoppingCarts(string userId,int ?restaurantId);

        Task<List<ShoppingCart>> GetShoppingCarts(Expression<Func<ShoppingCart, bool>> filter);

        Task DeleteShoppingCart(int? id);

        Task UpdateShoppingCart(ShoppingCart shoppingCart);

        Task<ShoppingCart> GetShoppingCart(int ?shoppingCartId);

        Task<ShoppingCart> GetShoppingCart(Expression<Func<ShoppingCart, bool>> filter);

         Task ClearUserShoppingCarts(string userId,int? restaurantId);
    }
}
