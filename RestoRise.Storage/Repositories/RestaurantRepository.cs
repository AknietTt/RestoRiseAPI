using AutoMapper;
using RestoRise.BuisnessLogic.ICrudRepository;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Repositories;

public class RestaurantRepository:BaseRepository<Restaurant>, IRestaurantRepositry
{
    public RestaurantRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

}