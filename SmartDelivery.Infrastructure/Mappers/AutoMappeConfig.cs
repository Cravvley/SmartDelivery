using AutoMapper;
using SmartDelivery.Data.Entities;
using SmartDelivery.Infrastructure.DTOs;
using SmartDelivery.Infrastructure.Models;

namespace SmartDelivery.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Restaurant, RestaurantDto>();
                cfg.CreateMap<Category, CategoryRequest>();
                cfg.CreateMap<Category, SubCategoryRequest>();
            }).CreateMapper();
    }
}
