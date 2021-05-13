using SmartDelivery.Data.Entities;
using System.Threading.Tasks;

namespace SmartDelivery.Infrastructure.Services.Interfaces
{
    public interface IOrderService
    {
        Task<int> CreateOrder(Order order, string userId, int restaurantId);

        Task<Order> GetOrder(int orderId);

        Task DeleteOrder(int orderId);
    }
}
