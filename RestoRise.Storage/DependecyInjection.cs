using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestoRise.BuisnessLogic.ICrudRepository;
using RestoRise.Domain.Entities;
using RestoRise.Storage.Repositories;

namespace RestoRise.Storage;

public static  class DependecyInjection
{
    public static IServiceCollection ImplementPersistence(this IServiceCollection services, IConfiguration configuration) {

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("RestoRise.Storage")), ServiceLifetime.Transient);

        services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IRestaurantRepositry, RestaurantRepository>();
        services.AddScoped<ICrudRepository<Restaurant>, RestaurantRepository>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICrudRepository<User>, UserRepository>();


        return services;
    }
}