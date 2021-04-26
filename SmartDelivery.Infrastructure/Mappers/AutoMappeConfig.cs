using AutoMapper;
using SmartDelivery.Data.Entities;
using SmartDelivery.Infrastructure.DTOs;

namespace SmartDelivery.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Restaurant, RestaurantDto>();

            }).CreateMapper();
    }
}
