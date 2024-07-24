using RestoRise.Application.DTOs.Order;
using RestoRise.Domain.Common;

namespace RestoRise.Application.Interfaces.Services;

public interface IOrderService
{
    Task<Result<bool>> CreateOrder(AddOrderDto dto);
    Task<Result<IEnumerable<OrderOutputDto>>> GetOredersByOwner(Guid ownerId);
}