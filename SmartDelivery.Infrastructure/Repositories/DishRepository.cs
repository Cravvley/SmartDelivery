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
    public class DishRepository:IDishRepository
    {
        private readonly AppDbContext _db;

        public DishRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Create(Dish product)
        {
            await _db.Dishes.AddAsync(product);

            await _db.SaveChangesAsync();
        }

        public async Task Delete(Dish product)
        {
            _db.Dishes.Remove(product);

            await _db.SaveChangesAsync();
        }

        public async Task<Dish> Get(int id)
            => await _db.Dishes.Include(s => s.Category).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IList<Dish>> GetAll(Expression<Func<Dish, bool>> filter)
            => await _db.Dishes.AsNoTracking().Include(s => s.Category)
                                .Where(filter).AsQueryable()
                                .ToListAsync();

        public async Task<IList<Dish>> GetPaginated(Expression<Func<Dish, bool>> filter, int pageSize = 1, int productPage = 1)
            => await _db.Dishes.AsNoTracking().Include(s => s.Category).Where(filter).AsQueryable().OrderBy(p => p.Title)
                                .Skip((productPage - 1) * pageSize)
                                .Take(pageSize).ToListAsync();

        public async Task Update(Dish product)
        {
            var productEntity = await Get(product.Id);

            productEntity.Title = product.Title;
            productEntity.Description = product.Description;
            productEntity.GrossPrice = product.GrossPrice;
            productEntity.Image = product.Image;
            productEntity.CategoryId = product.CategoryId;

            await _db.SaveChangesAsync();
        }
    }
}
