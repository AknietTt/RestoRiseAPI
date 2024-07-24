using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestoRise.Application.Interfaces.Repositories;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Repositories;

public class FoodRepository:BaseRepository<Food>, IFoodRepository
{
    public FoodRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ICollection<Food>> GetFoodsByRestaurantId(Guid restaurantId)
    {
        return await _context.Foods.Where(x => x.Restaurant.Id == restaurantId).ToListAsync();
        
    }
}