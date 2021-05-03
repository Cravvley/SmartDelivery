﻿using Microsoft.EntityFrameworkCore;
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
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly AppDbContext _db;

        public RestaurantRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Create(Restaurant restaurant)
        {
            await _db.Restaurant.AddAsync(restaurant);

            await _db.SaveChangesAsync();
        }

        public async Task Delete(Restaurant restaurant)
        {
            _db.Restaurant.Remove(restaurant);

            await _db.SaveChangesAsync();
        }

        public async Task<Restaurant> Get(int id)
            => await _db.Restaurant.Include(a=>a.Address).SingleOrDefaultAsync(s => s.Id == id);

        public async Task<Restaurant> Get(Expression<Func<Restaurant, bool>> filter)
            => await _db.Restaurant.Include(a => a.Address).FirstOrDefaultAsync(filter);

        public async Task<IList<Restaurant>> GetAll(Expression<Func<Restaurant, bool>> filter)
            => await _db.Restaurant.Include(a => a.Address).AsNoTracking().Where(filter).AsQueryable().ToListAsync();

        public async Task<IList<Restaurant>> GetPaginated(Expression<Func<Restaurant, bool>> filter, int pageSize = 1, int productPage = 1)
        => await _db.Restaurant.Include(a => a.Address).AsNoTracking().Where(filter).AsQueryable().OrderBy(p => p.Name)
                                     .Skip((productPage - 1) * pageSize)
                                     .Take(pageSize).ToListAsync();

        public async Task Update(Restaurant restauran)
        {
            var restauranEntity = await Get(restauran.Id);
            _db.Entry(restauranEntity).CurrentValues.SetValues(restauran);

            await _db.SaveChangesAsync();
        }
    }
}
