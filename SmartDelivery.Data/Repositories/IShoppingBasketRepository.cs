using SmartDelivery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartDelivery.Data.Repositories
{
    public interface IShoppingBasketRepository
    {
        Task AddShoppingCart(ShoppingCart shoppingCart);

        Task<List<ShoppingCart>> GetShoppingCarts(string userId,int restaurantId);

        Task<ShoppingCart> GetShoppingCart(int id);

        Task<ShoppingCart> GetShoppingCart(Expression<Func<ShoppingCart, bool>> filter);

        Task UpdateShoppingCart(ShoppingCart shoppingCart);

        Task DeleteShoppingCart(ShoppingCart shoppingCart);

        Task DeleteShoppingCartRange(IEnumerable<ShoppingCart> shoppingCart);
    }
}
