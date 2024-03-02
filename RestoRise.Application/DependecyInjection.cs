using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestoRise.BuisnessLogic.Helpers.Mappers;
using RestoRise.BuisnessLogic.Interfaces;
using RestoRise.BuisnessLogic.Services;

namespace RestoRise.BuisnessLogic;

public static class DependecyInjection
{
    public static IServiceCollection ImplementService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(RestaurantMapping).Assembly);
        services.AddAutoMapper(typeof(UserMapping).Assembly);
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRestaurnatService , RestaurantService>();
        

        return services;
    }
}