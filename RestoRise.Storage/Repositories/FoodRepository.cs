using AutoMapper;
using RestoRise.Application.Interfaces.Repositories;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Repositories;

public class FoodRepository:BaseRepository<Food>, IFoodRepository
{
    public FoodRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}