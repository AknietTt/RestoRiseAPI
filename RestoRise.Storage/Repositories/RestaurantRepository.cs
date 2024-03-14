using AutoMapper;
using RestoRise.Application.Interfaces.Repositories;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Repositories;

public class RestaurantRepository:BaseRepository<Restaurant>, IRestaurantRepositry
{
    public RestaurantRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

}