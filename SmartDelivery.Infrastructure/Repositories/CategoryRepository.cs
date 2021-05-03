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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;

        public CategoryRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Create(Category category)
        {
            await _db.Categories.AddAsync(category);

            await _db.SaveChangesAsync();
        }

        public async Task Delete(Category category)
        {
            var subcategoryList = await GetAll(x => x.ParentId == category.Id);

            foreach (var subcategory in subcategoryList)
            {
                await Delete(subcategory);
            }

            _db.Categories.Remove(category);

            await _db.SaveChangesAsync();
        }

        public async Task<Category> Get(int id)
            => await _db.Categories.Include(s => s.CategoryParent)
                      .SingleOrDefaultAsync(s => s.Id == id);

        public async Task<IList<Category>> GetAll(Expression<Func<Category, bool>> filter)
            => await _db.Categories.AsNoTracking().Where(filter).OrderBy(p => p.Title).AsQueryable().ToListAsync();

        public async Task<IList<Category>> GetWithParentId(int id)
            => await _db.Categories.Include(s => s.CategoryParent)
                      .Where(s => s.ParentId == id).ToListAsync();

        public async Task Update(Category category)
        {
            var categoryEntity = await Get(category.Id);

            categoryEntity.Title = category.Title;

            await _db.SaveChangesAsync();
        }
    }
}
