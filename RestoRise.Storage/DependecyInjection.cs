using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestoRise.Application.Interfaces.Repositories;
using RestoRise.Domain.Entities;
using RestoRise.Storage.Repositories;

namespace RestoRise.Storage;

public static  class DependecyInjection
{
    public static IServiceCollection ImplementPersistence(this IServiceCollection services, IConfiguration configuration) {

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("RestoRise.Storage")));

        services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IRestaurantRepositry, RestaurantRepository>();
        services.AddScoped<ICrudRepository<Restaurant>, RestaurantRepository>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICrudRepository<User>, UserRepository>();

        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<ICrudRepository<City>, CityRepository>();

        services.AddScoped<IMenuRepository, MenuRepository>();
        services.AddScoped<ICrudRepository<Menu>, MenuRepository>();

        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<ICrudRepository<Branch>, BranchRepository>();
        
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICrudRepository<Category>, CategoryRepository>();

        services.AddScoped<IFoodRepository, FoodRepository>();
        services.AddScoped<ICrudRepository<Food>, FoodRepository>();

        return services;
    }
}