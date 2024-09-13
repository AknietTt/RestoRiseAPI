using AutoMapper;
using RestoRise.Application.Interfaces.Repositories;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Repositories;

public class RoleRepository:BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}