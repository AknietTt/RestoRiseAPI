using AutoMapper;
using RestoRise.Application.DTOs.Order;
using RestoRise.Application.Interfaces.Repositories;
using RestoRise.Application.Interfaces.Services;
using RestoRise.Domain.Common;
using RestoRise.Domain.Entities;
using RestoRise.Domain.Enums;

namespace RestoRise.BuisnessLogic.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<bool>> CreateOrder(AddOrderDto dto)
    {
        try
        {
            var orderRepository = _unitOfWork.GetRepository<Order>();
            var branchRepository = _unitOfWork.GetRepository<Branch>();
            var foodRepository = _unitOfWork.GetRepository<Food>();
            var orderDetailRepository = _unitOfWork.GetRepository<OrderDetail>();

            var branch = await branchRepository.FirstOrDefault(x => x.Id == dto.BranchId);
            branchRepository.Attach(branch);

            // Create new order
            var order = new Order
            {
                CreateAt = DateTimeOffset.UtcNow,
                Summa = dto.Summa,
                Address = dto.Address,
                NameCustomer = dto.NameCustomer,
                PhoneNumber = dto.PhoneNumber,
                Entrance = dto.Entrance,
                Intercom = dto.Intercom,
                Comment = dto.Comment,
                Status = OrderStatus.Pending ,
                Branch =branch ,
                OrderDetails = new List<OrderDetail>()
            };
            // Add order details
            foreach (var detailDto in dto.OrderDetailDtos)
            {
                var food = await foodRepository.GetWithIncludesAsync(detailDto.FoodId, f => f.Restaurant,
                    f => f.Restaurant.Branches);
                if (food == null) return Result<bool>.Failure($"Food with ID {detailDto.FoodId} not found.", 400);

                var orderDetail = new OrderDetail
                {
                    Order = order,
                    Food = food,
                    Count = detailDto.Count
                };
                order.OrderDetails.Add(orderDetail);
            }

            await orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"An error occurred while creating the order: {ex.Message}", 500);
        }
    }



    public async Task<Result<IEnumerable<OrderOutputDto>>> GetOrdersByUserId(Guid ownerId, int statusCode)
{
    try
    {
        // Получение репозиториев ресторанов, филиалов и сотрудников
        var restaurantRepository = _unitOfWork.GetRepository<Restaurant>();
        var staffRepository = _unitOfWork.GetRepository<Staff>();
        var branchRepository = _unitOfWork.GetRepository<Branch>();
        var orderRepository = _unitOfWork.GetRepository<Order>();

        // Получение ресторанов владельца
        var ownerRestaurants = await restaurantRepository.GetAsync(x => x.Owner.Id == ownerId);

        // Если рестораны владельца не найдены, ищем заказы через филиал сотрудника
        if (ownerRestaurants == null || !ownerRestaurants.Any())
        {
            // Получение сотрудника по идентификатору пользователя (ownerId)
            var staffMember = await staffRepository.FirstOrDefault(s => s.Id == ownerId, includeProperties: new[] { "Branch.Restaurant" });

            if (staffMember == null)
            {
                return Result<IEnumerable<OrderOutputDto>>.Failure("No restaurants or staff found for the given user.", 404);
            }

            // Получение ресторана через филиал сотрудника
            var staffRestaurant = staffMember.Branch.Restaurant;

            if (staffRestaurant == null)
            {
                return Result<IEnumerable<OrderOutputDto>>.Failure("No restaurant found for the staff member.", 404);
            }

            // Проверка статуса заказа
            if (!Enum.TryParse(statusCode.ToString(), out OrderStatus status))
            {
                return Result<IEnumerable<OrderOutputDto>>.Failure("Invalid status code", 400);
            }

            // Получение заказов, связанных с рестораном филиала сотрудника
            var staffOrders = await orderRepository.GetAsync(
                o => o.OrderDetails.Any(od => od.Food.Restaurant.Id == staffRestaurant.Id && o.Status == status),
                includeProperties: new[] { "Branch", "OrderDetails.Food" }
            );

            // Маппинг заказов в DTO
            var staffOrderOutputDtos = _mapper.Map<IEnumerable<OrderOutputDto>>(staffOrders);
            return Result<IEnumerable<OrderOutputDto>>.Success(staffOrderOutputDtos);
        }

        // Проверка статуса заказа для ресторанов владельца
        if (!Enum.TryParse(statusCode.ToString(), out OrderStatus ownerStatus))
        {
            return Result<IEnumerable<OrderOutputDto>>.Failure("Invalid status code", 400);
        }

        // Получение идентификаторов ресторанов владельца
        var restaurantIds = ownerRestaurants.Select(r => r.Id).ToList();

        // Получение заказов, связанных с ресторанами владельца
        var ownerOrders = await orderRepository.GetAsync(
            o => o.OrderDetails.Any(od => restaurantIds.Contains(od.Food.Restaurant.Id) && o.Status == ownerStatus),
            includeProperties: new[] { "Branch", "OrderDetails.Food" }
        );

        // Маппинг заказов в DTO
        var ownerOrderOutputDtos = _mapper.Map<IEnumerable<OrderOutputDto>>(ownerOrders);

        return Result<IEnumerable<OrderOutputDto>>.Success(ownerOrderOutputDtos);
    }
    catch (Exception ex)
    {
        return Result<IEnumerable<OrderOutputDto>>.Failure($"An error occurred while retrieving the orders: {ex.Message}", 500);
    }
}

    public async Task<Result<OrderOutputDto>> GetOrderDetail(Guid orderId)
    {
        var repository =  _unitOfWork.GetRepository<Order>();
        var order = await repository.FirstOrDefault(x => x.Id == orderId,
            includeProperties: new[] { "Branch", "OrderDetails.Food" });
        if (order == null)
        {
            return Result<OrderOutputDto>.Failure(  "Order not found", 404);     
        }
        var result = _mapper.Map<OrderOutputDto>(order);
        return Result<OrderOutputDto>.Success(result);
    }

    public async Task<Result<bool>> EditStatusOrder(Guid orderId, int statusCode)
    {
        var repository = _unitOfWork.GetRepository<Order>();
        var order = await repository.FirstOrDefault(x => x.Id == orderId);
    
        if (order == null)
        {
            return Result<bool>.Failure("Order not found" , 404);
        }

        if (!Enum.IsDefined(typeof(OrderStatus), statusCode))
        {
            return Result<bool>.Failure("Invalid status code", 400);
        }
        order.Status = (OrderStatus)statusCode;
        // repository.Attach(order);
        repository.Update(order);
        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }

}