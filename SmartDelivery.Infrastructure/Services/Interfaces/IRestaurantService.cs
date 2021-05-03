using SmartDelivery.Data.Entities;
using SmartDelivery.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartDelivery.Infrastructure.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task Create(Restaurant restaurant);

        Task Delete(int? id);

        Task Update(Restaurant restaurant);

        Task<Restaurant> Get(int? id);

        Task<bool> Exist(Expression<Func<Restaurant, bool>> filter);

        Task<IList<Restaurant>> GetAllDetails(Expression<Func<Restaurant, bool>> filter = null);

        Task<IList<RestaurantDto>> GetAllHeaders(Expression<Func<Restaurant, bool>> filter = null);

        Task<IList<RestaurantDto>> GetPaginatedHeaders(Expression<Func<Restaurant, bool>> filter = null, int pageSize = 0, int productPage = 0);

        Task<(IList<RestaurantDto> restaurants, int restaurantsCount)> GetFiltered(string restaurantName = null, string cityName = null, int? pageSize = null, int? productPage = null);
    }
}
