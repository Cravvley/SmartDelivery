using SmartDelivery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartDelivery.Data.Repositories
{
    public interface IOrderRepository
    {
        Task Create(Order order);

        Task<IList<Order>> GetAll(Expression<Func<Order, bool>> filter);

        Task<Order> GetOne(Expression<Func<Order, bool>> filter);

        Task<Order> GetById(int id);

        Task Update(Order order);

        Task CancelOrder(Order order);
    }
}
