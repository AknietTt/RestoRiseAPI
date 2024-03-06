using AutoMapper;
using RestoRise.BuisnessLogic.ICrudRepository;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Repositories;

public class CityRepository:BaseRepository<City>, ICityRepository
{
    public CityRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}