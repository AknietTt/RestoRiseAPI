using AutoMapper;
using RestoRise.Application.Interfaces.Repositories;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Repositories;

public class CategoryRepository:BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}