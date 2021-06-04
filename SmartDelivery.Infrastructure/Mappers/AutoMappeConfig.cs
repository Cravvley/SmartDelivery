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
                cfg.CreateMap<Restaurant, RestaurantDto>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.ZipCode))
                .ForMember(dest => dest.StreetAddress, opt => opt.MapFrom(src => src.Address.Street));
                cfg.CreateMap<Category, CategoryRequest>();
                cfg.CreateMap<Category, SubCategoryRequest>();
            }).CreateMapper();
    }
}
