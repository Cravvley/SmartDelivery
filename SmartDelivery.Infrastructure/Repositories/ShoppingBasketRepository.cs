using Microsoft.EntityFrameworkCore;
using SmartDelivery.Data.EF;
using SmartDelivery.Data.Entities;
using SmartDelivery.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartDelivery.Infrastructure.Repositories
{
    public class ShoppingBasketRepository:IShoppingBasketRepository
    {
        private readonly AppDbContext _db;

        public ShoppingBasketRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddShoppingCart(ShoppingCart shoppingCart)
        {
            await _db.ShoppingCarts.AddAsync(shoppingCart);

            await _db.SaveChangesAsync();
        }

        public async Task<List<ShoppingCart>> GetShoppingCarts(Expression<Func<ShoppingCart, bool>> filter)
            => await _db.ShoppingCarts.Include(x => x.User).Include(s=>s.Restaurant)
                                      .Include(x => x.Dish).ThenInclude(x => x.Category)
                                       .Where(filter).ToListAsync();

        public async Task<ShoppingCart> GetShoppingCart(int id)
            => await _db.ShoppingCarts.Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            var shoppingCartEntity = await GetShoppingCart(shoppingCart.Id);
            _db.Entry(shoppingCartEntity).CurrentValues.SetValues(shoppingCart);

            await _db.SaveChangesAsync();
        }

        public async Task DeleteShoppingCart(ShoppingCart shoppingCart)
        {
            _db.ShoppingCarts.Remove( shoppingCart);

            await _db.SaveChangesAsync();
        }

        public async Task DeleteShoppingCartRange(IEnumerable<ShoppingCart> shoppingCart)
        {
            _db.ShoppingCarts.RemoveRange(shoppingCart);

            await _db.SaveChangesAsync();
        }

        public async Task<ShoppingCart> GetShoppingCart(Expression<Func<ShoppingCart, bool>> filter)
            => await _db.ShoppingCarts.SingleOrDefaultAsync(filter);
    }
}

