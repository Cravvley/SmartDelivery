using SmartDelivery.Data.Entities;
using SmartDelivery.Data.Enums;
using SmartDelivery.Data.Repositories;
using SmartDelivery.Infrastructure.Models;
using SmartDelivery.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
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

        public Task CancelOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Create(Order order, string userId,int restaurantId)
        {
            var productsFromBasket = await _shoppingBasketRepository.GetShoppingCarts(s => s.UserId == userId && s.RestaurantId == restaurantId);
            var orderItems = productsFromBasket.Select(x => new OrderItem
            {
                Count = x.Count,
                DishId = x.DishId,
            });

            order.CreateAt = DateTime.Now;
            order.OrderStatus = EnumOrderStatus.New;
            order.PaymentStatus = EnumPaymentStatus.Waiting;
            order.TotalGrossPrice = productsFromBasket.Sum(x => x.Dish.GrossPrice * x.Count);
            order.TotalQuantity = productsFromBasket.Sum(x => x.Count);
            order.UserId = userId;
            order.OrderItems = orderItems.ToList();

            await _orderRepository.Create(order);

            return order.Id;
        }

        public Task FinishOrder(int orderId, FinishRequest request, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOne(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task LoadOrder(int orderId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task LoadOrder(IEnumerable<int> order, int userId)
        {
            throw new NotImplementedException();
        }

        public Task Pay(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStatus(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
