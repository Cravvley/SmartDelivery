using SmartDelivery.Data.Entities;
using SmartDelivery.Data.Repositories;
using SmartDelivery.Infrastructure.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDelivery.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IShoppingBasketRepository _shoppingBasketRepository;
        private readonly IOrderRepository _orderRepository;
        public OrderService(IShoppingBasketRepository shoppingBasketRepository, IOrderRepository orderRepository)
        {
            _shoppingBasketRepository = shoppingBasketRepository;
            _orderRepository = orderRepository;
        }

        public async Task<int> CreateOrder(Order order, string userId, int restaurantId)
        {
            var productsFromBasket = await _shoppingBasketRepository.GetShoppingCarts(s => s.UserId == userId && s.RestaurantId == restaurantId);
            var orderItems = productsFromBasket.Select(x => new OrderItem
            {
                Count = x.Count,
                DishId = x.DishId,
            });

            order.CreateAt = DateTime.Now;
            order.TotalGrossPrice = productsFromBasket.Sum(x => x.Dish.GrossPrice * x.Count);
            order.TotalQuantity = productsFromBasket.Sum(x => x.Count);
            order.UserId = userId;
            order.OrderItems = orderItems.ToList();

            await _orderRepository.Create(order);

            return order.Id;
        }

        public async Task DeleteOrder(int orderId)
        {
            var orderEntity = await _orderRepository.Get(orderId);

            if(orderEntity is null)
            {
                return;
            }

            await _orderRepository.Remove(orderEntity);
        }

        public async Task<Order> GetOrder(int orderId)
        {
            var orderEntity = await _orderRepository.Get(orderId);

            if(orderEntity is null)
            {
                throw new ArgumentNullException("Order doesn't exist");
            }

            return orderEntity;
        }
    }
}
