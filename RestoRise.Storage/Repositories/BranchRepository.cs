using AutoMapper;
using RestoRise.BuisnessLogic.ICrudRepository;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Repositories;

public class BranchRepository:BaseRepository<Branch> , IBranchRepository
{
    public BranchRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}