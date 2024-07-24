using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestoRise.Application.Interfaces.Repositories;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Repositories;

public class RestaurantRepository:BaseRepository<Restaurant>, IRestaurantRepositry
{
    public RestaurantRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ICollection<Restaurant>> GetRestaurantsByCity(Guid cityId)
    {
        var restaurants = await _context.Restaurants
            .Include(r => r.Branches).ThenInclude(b=>b.City)
            .Where(r => r.Branches.Any(b => b.City.Id == cityId))
            .ToListAsync();

        return restaurants;
    }
}