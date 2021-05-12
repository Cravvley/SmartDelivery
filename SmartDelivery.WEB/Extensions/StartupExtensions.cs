using Microsoft.Extensions.DependencyInjection;
using SmartDelivery.Data.Repositories;
using SmartDelivery.Infrastructure.Repositories;
using SmartDelivery.Infrastructure.Services;
using SmartDelivery.Infrastructure.Services.Interfaces;

namespace SmartDelivery.WEB.Extensions
{
    public static class StartupExtensions
    {
        public static void AddOurServices(this IServiceCollection services)
        {
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IDishService, DishService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IShoppingBasketService, ShoppingBasketService>();
        }

        public static void AddOurRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IShoppingBasketRepository, ShoppingBasketRepository>();
        }
    }
}
