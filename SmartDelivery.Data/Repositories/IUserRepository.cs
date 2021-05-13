using SmartDelivery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartDelivery.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> Get(string id);

        Task<User> Get(Expression<Func<User, bool>> filter);

        Task<IList<User>> GetAll(Expression<Func<User, bool>> filter = null);

        Task Update(User user);

        Task Delete(User user);

        Task<IList<User>> GetPaginated(Expression<Func<User, bool>> filter, int pageSize = 1, int productPage = 1);
    }
}
