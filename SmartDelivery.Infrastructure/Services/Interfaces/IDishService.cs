using SmartDelivery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartDelivery.Infrastructure.Services.Interfaces
{
    public interface IDishService
    {
        Task Create(Dish product);
        Task Delete(int? id);
        Task<Dish> Get(int? id);
        Task<IList<Dish>> GetAll(Expression<Func<Dish, bool>> filter = null);
        Task<(IList<Dish> products, int productsCount)> GetFiltered(string productName = null, string categoryName = null, int? pageSize = null, int? productPage = null);
        Task<IList<Dish>> GetPaginated(Expression<Func<Dish, bool>> filter = null, int pageSize = 1, int productPage = 1);
        Task Update(Dish product);
    }
}
