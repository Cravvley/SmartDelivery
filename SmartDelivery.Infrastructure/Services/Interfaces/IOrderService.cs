using SmartDelivery.Data.Entities;
using SmartDelivery.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDelivery.Infrastructure.Services.Interfaces
{
    public interface IOrderService
    {
        Task<int> Create(Order order, string userId, int restaurantId);

        Task<Order> GetOne(int orderId);

        Task Pay(int id);

        Task LoadOrder(int orderId, int userId);

        Task LoadOrder(IEnumerable<int> order, int userId);

        Task FinishOrder(int orderId, FinishRequest request, int userId);

        Task CancelOrder(int orderId);

        Task UpdateStatus(int orderId);
    }
}
