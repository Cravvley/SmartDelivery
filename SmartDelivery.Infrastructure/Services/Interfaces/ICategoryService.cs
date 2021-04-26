using SmartDelivery.Data.Entities;
using SmartDelivery.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartDelivery.Infrastructure.Services.Interfaces
{
    public interface ICategoryService
    {
        Task Create(Category category);

        Task Delete(int? id);

        Task<Category> Get(int? id);

        Task<IList<Category>> GetWithParentId(int? id);

        Task<IList<Category>> GetAll(Expression<Func<Category, bool>> filter = null);

        Task Update(Category category);

        Task<IList<CategoryRequest>> GetAllCategoryWithSubCategory();
    }
}
