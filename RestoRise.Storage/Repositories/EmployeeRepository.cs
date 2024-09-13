using AutoMapper;
using RestoRise.Application.Interfaces.Repositories;
using RestoRise.Application.Interfaces.Services;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Repositories;

public class EmployeeRepository:BaseRepository<Staff>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}