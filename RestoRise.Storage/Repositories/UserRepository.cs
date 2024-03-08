using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestoRise.BuisnessLogic.ICrudRepository;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Repositories;

public class UserRepository:BaseRepository<User> , IUserRepository
{
    public UserRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

}