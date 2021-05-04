using SmartDelivery.Data.Entities;
using SmartDelivery.Data.Repositories;
using SmartDelivery.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartDelivery.Infrastructure.Services
{

    public class DishService : IDishService
    {
        private readonly IDishRepository _dishRepository;

        public DishService(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }
        public async Task Create(Dish product)
        {
            if (product is null)
            {
                throw new ArgumentNullException("Dish doesn't exist");
            }
            await _dishRepository.Create(product);
        }

        public async Task Delete(int? id)
        {
            var productEntity = await _dishRepository.Get(id.Value);
            if (productEntity is null)
            {
                return;
            }

            await _dishRepository.Delete(productEntity);
        }

        public async Task<Dish> Get(int? id)
        {
            var productEntity = await _dishRepository.Get(id.Value);

            if (productEntity is null)
            {
                throw new ArgumentNullException("Dish doesn't exist");
            }
            return productEntity;
        }

        public async Task<IList<Dish>> GetPaginated(Expression<Func<Dish, bool>> filter = null, int pageSize = 1, int productPage = 1)
        {
            if (filter is null)
            {
                return await _dishRepository.GetPaginated(p => true, pageSize, productPage);
            }

            return await _dishRepository.GetPaginated(filter, pageSize, productPage);
        }

        public async Task<IList<Dish>> GetAll(Expression<Func<Dish, bool>> filter = null)
        {
            if (filter is null)
            {
                return await _dishRepository.GetAll(p => true);
            }

            return await _dishRepository.GetAll(filter);
        }

        public async Task Update(Dish product)
        {
            var productEntity = await _dishRepository.Get(product.Id);
            if (productEntity is null)
            {
                throw new ArgumentNullException("Dish doesn't exist");
            }
            await _dishRepository.Update(product);
        }

        public async Task<(IList<Dish> products, int productsCount)> GetFiltered(string productName = null, string categoryName = null, int? pageSize = null, int? productPage = null)
        {
            var products = await GetAll();

            if (!(productName is null))
            {
                products = products.Where(p => p.Title.ToLower().Contains(productName.ToLower())).ToList();
            }
            if (!(categoryName is null))
            {
                products = products.Where(p => p.Category.Title.ToLower().Contains(categoryName.ToLower())).ToList();
            }
            return (products.OrderBy(p => p.Title).Skip((productPage.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList(), products.Count);
        }
    }
}
