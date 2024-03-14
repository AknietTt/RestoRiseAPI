using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestoRise.Application.Helpers.Mappers;
using RestoRise.Application.Interfaces.Services;
using RestoRise.BuisnessLogic.Services;

namespace RestoRise.Application;

public static class DependecyInjection
{
    public static IServiceCollection ImplementService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(RestaurantMapping).Assembly);
        services.AddAutoMapper(typeof(UserMapping).Assembly);
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRestaurnatService , RestaurantService>();

        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IBranchService, BranchService>();

        services.AddScoped<IFoodService, FoodService>();
        return services;
    }
}