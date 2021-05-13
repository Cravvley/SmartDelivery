using Microsoft.EntityFrameworkCore;
using SmartDelivery.Data.EF;
using SmartDelivery.Data.Entities;
using SmartDelivery.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartDelivery.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private readonly AppDbContext _db;

        public OrderRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Create(Order order)
        {
            await _db.Orders.AddAsync(order);

            await _db.SaveChangesAsync();
        }

        public async Task<IList<Order>> GetAll(Expression<Func<Order, bool>> filter)
            => await _db.Orders.AsNoTracking().Include(s => s.OrderItems).ThenInclude(x => x.Dish).Include(x => x.User)
                                .Where(filter).AsQueryable()
                                .ToListAsync();

        public async Task<Order> GetOne(Expression<Func<Order, bool>> filter)
            => await _db.Orders.AsNoTracking().Include(s => s.OrderItems).ThenInclude(x => x.Dish).Include(x => x.User)
                                .SingleOrDefaultAsync(filter);

        public async Task<Order> GetById(int id)
        {
            return await _db.Orders.SingleAsync(x => x.Id == id);
        }

        public async Task Update(Order order)
        {
            _db.Orders.Update(order);

            await _db.SaveChangesAsync();
        }

        public async Task CancelOrder(Order order)
        {
            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();
        }
    }
}
