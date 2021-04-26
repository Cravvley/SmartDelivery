using SmartDelivery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartDelivery.Data.Repositories
{
    public interface ICategoryRepository
    {
        Task Create(Category category);

        Task Delete(Category category);

        Task Update(Category category);

        Task<Category> Get(int id);

        Task<IList<Category>> GetWithParentId(int id);

        Task<IList<Category>> GetAll(Expression<Func<Category, bool>> filter);
    }
}
