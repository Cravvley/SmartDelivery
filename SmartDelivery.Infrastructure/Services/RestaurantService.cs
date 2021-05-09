using AutoMapper;
using SmartDelivery.Data.Entities;
using SmartDelivery.Data.Repositories;
using SmartDelivery.Infrastructure.DTOs;
using SmartDelivery.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartDelivery.Infrastructure.Services
{
    public class RestaurantService:IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        public async Task Create(Restaurant restaurant)
        {
            if (restaurant is null)
            {
                throw new ArgumentNullException("restaurant doesn't exist");
            }

            await _restaurantRepository.Create(restaurant);
        }

        public async Task Delete(int? id)
        {
            var restaurantEntity = await _restaurantRepository.Get(id.Value);
            if (restaurantEntity is null)
            {
                return;
            }

            await _restaurantRepository.Delete(restaurantEntity);
        }

        public async Task<Restaurant> Get(int? id)
        {
            var restaurantEntity = await _restaurantRepository.Get(id.Value);
            if (restaurantEntity is null)
            {
                throw new ArgumentNullException("restaurant doesn't exist");
            }

            return restaurantEntity;
        }

        public async Task<IList<Restaurant>> GetAllDetails(Expression<Func<Restaurant, bool>> filter = null)
        {
            if (filter is null)
            {
                return await _restaurantRepository.GetAll(s => true);
            }

            return await _restaurantRepository.GetAll(filter);
        }


        public async Task<IList<RestaurantDto>> GetAllHeaders(Expression<Func<Restaurant, bool>> filter = null)
        {
            if (filter is null)
            {
                return _mapper.Map<IList<Restaurant>, IList<RestaurantDto>>(await _restaurantRepository.GetAll(s => true));
            }

            return _mapper.Map<IList<Restaurant>, IList<RestaurantDto>>(await _restaurantRepository.GetAll(filter));
        }

        public async Task<IList<RestaurantDto>> GetPaginatedHeaders(Expression<Func<Restaurant, bool>> filter = null, int pageSize = 1, int productPage = 1)
        {
            if (filter is null)
            {
                return _mapper.Map<IList<Restaurant>, IList<RestaurantDto>>(await _restaurantRepository.GetPaginated(s => true, pageSize, productPage));
            }

            return _mapper.Map<IList<Restaurant>, IList<RestaurantDto>>(await _restaurantRepository.GetPaginated(filter, pageSize, productPage));
        }
        public async Task<bool> Exist(Expression<Func<Restaurant, bool>> filter)
        {
            var restaurantEntity = await _restaurantRepository.Get(filter);
            if (restaurantEntity is null)
            {
                return false;
            }

            return true;
        }

        public async Task Update(Restaurant restaurant)
        {
            var restaurantEntity = await _restaurantRepository.Get(restaurant.Id);
            if (restaurantEntity is null)
            {
                throw new ArgumentNullException("restaurant doesn't exist");
            }

            await _restaurantRepository.Update(restaurant);
        }

        public async Task<(IList<RestaurantDto> restaurants, int restaurantsCount)> GetFiltered(string restaurantName = null, string cityName = null, int? pageSize = null, int? productPage = null)
        {
            var restaurants = await GetAllHeaders();

            if (restaurantName is null && cityName is null)
            {
                return (await GetPaginatedHeaders(s => true, pageSize.Value, productPage.Value), restaurants.Count);
            }
            else if (!(restaurantName is null || cityName is null))
            {
                restaurants = await GetAllHeaders(s => s.Name.ToLower()
                                    .Contains(restaurantName.ToLower()) && s.Address.City.ToLower()
                                    .Contains(cityName.ToLower()));

                return (await GetPaginatedHeaders(s => s.Name.ToLower()
                                    .Contains(restaurantName.ToLower()) && s.Address.City.ToLower()
                                    .Contains(cityName.ToLower()), pageSize.Value, productPage.Value), restaurants.Count);
            }
            else if (!(restaurantName is null))
            {
                restaurants = await GetAllHeaders(s => s.Name.ToLower().Contains(restaurantName.ToLower()));

                return (await GetPaginatedHeaders(s => s.Name.ToLower().Contains(restaurantName.ToLower()), pageSize.Value, productPage.Value), restaurants.Count);
            }
            else if (!(cityName is null))
            {
                restaurants = await GetAllHeaders(s => s.Address.City.ToLower().Contains(cityName.ToLower()));

                return (await GetPaginatedHeaders(s => s.Address.City.ToLower().Contains(cityName.ToLower()), pageSize.Value, productPage.Value), restaurants.Count);
            }

            return (restaurants, restaurants.Count);
        }

        public async Task AddWorker(int ?restaurantId, User worker)
        {
            var restaurantEntity = await _restaurantRepository.Get(restaurantId.Value);
            if (restaurantEntity is null)
            {
                throw new ArgumentNullException("restaurant doesn't exist");
            }
            
            restaurantEntity.Employees.Add(worker);
            await _restaurantRepository.Update(restaurantEntity);
        }

        public async Task<IList<User>> GetWorkers(int ? restaurantId)
        {
            var restaurantEntity = await _restaurantRepository.Get(restaurantId.Value);
            if (restaurantEntity is null)
            {
                throw new ArgumentNullException("restaurant doesn't exist");
            }
            return restaurantEntity.Employees;
        }

        public Task<Restaurant> GetRestaurantByWorker(int? id)
        {
            return null;
        }
    }
}
