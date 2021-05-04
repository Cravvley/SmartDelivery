using SmartDelivery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartDelivery.Data.Repositories
{
    public interface IDishRepository
    {
        Task Create(Dish product);

        Task Delete(Dish product);

        Task Update(Dish product);

        Task<Dish> Get(int id);

        Task<IList<Dish>> GetAll(Expression<Func<Dish, bool>> filter);

        Task<IList<Dish>> GetPaginated(Expression<Func<Dish, bool>> filter, int pageSize = 1, int productPage = 1);
    }
}
