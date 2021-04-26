using SmartDelivery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartDelivery.Data.Repositories
{
    public interface IRestaurantRepository
    {
        Task Create(Restaurant restaurant);

        Task Delete(Restaurant restaurant);

        Task Update(Restaurant restaurant);

        Task<Restaurant> Get(int id);

        Task<Restaurant> Get(Expression<Func<Restaurant, bool>> filter);

        Task<IList<Restaurant>> GetAll(Expression<Func<Restaurant, bool>> filter);

        Task<IList<Restaurant>> GetPaginated(Expression<Func<Restaurant, bool>> filter, int pageSize = 1, int productPage = 1);
    }
}
