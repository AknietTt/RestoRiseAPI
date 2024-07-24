using AutoMapper;
using RestoRise.Application.Interfaces.Repositories;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
    