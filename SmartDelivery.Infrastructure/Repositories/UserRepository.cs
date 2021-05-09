using Microsoft.EntityFrameworkCore;
using SmartDelivery.Data.EF;
using SmartDelivery.Data.Entities;
using SmartDelivery.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartDelivery.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User> Get(string id)
                => await _db.User.SingleOrDefaultAsync(u => u.Id == id);

        public async Task<IList<User>> GetAll(Expression<Func<User, bool>> filter = null)
            => await _db.User.AsNoTracking().Where(filter).AsQueryable().ToListAsync();

        public async Task<IList<User>> GetPaginated(Expression<Func<User, bool>> filter, int pageSize = 0, int productPage = 0)
         => await _db.User.AsNoTracking().Where(filter).AsQueryable().OrderBy(p => p.UserName)
                                     .Skip((productPage - 1) * pageSize)
                                     .Take(pageSize).ToListAsync();
        public async Task Delete(User user)
        {
            _db.User.Remove(user);

            await _db.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            var userEntity = await Get(user.Id);

            userEntity.LockoutEnd = user.LockoutEnd;

            await _db.SaveChangesAsync();
        }
    }
}
