using RestoRise.Application.DTOs.Order;
using RestoRise.Domain.Common;

namespace RestoRise.Application.Interfaces.Services;

public interface IOrderService
{
    Task<Result<bool>> CreateOrder(AddOrderDto dto);
    Task<Result<IEnumerable<OrderOutputDto>>> GetOrdersByUserId(Guid ownerId, int statusCode);
    Task<Result<OrderOutputDto>> GetOrderDetail(Guid ownerId);
    Task<Result<bool>> EditStatusOrder(Guid orderId, int statusCode);
}