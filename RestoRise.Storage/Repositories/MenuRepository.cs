using AutoMapper;
using RestoRise.Application.Interfaces.Repositories;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Repositories;

public class MenuRepository:BaseRepository<Menu>, IMenuRepository
{
    public MenuRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}