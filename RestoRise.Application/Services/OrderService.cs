using AutoMapper;
using RestoRise.Application.DTOs.Order;
using RestoRise.Application.Interfaces.Repositories;
using RestoRise.Application.Interfaces.Services;
using RestoRise.Domain.Common;
using RestoRise.Domain.Entities;

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

            // Select a random branch from the collected branches
            if (order.OrderDetails.SelectMany(od => od.Food.Restaurant.Branches).Any())
            {
                var allBranches = order.OrderDetails.SelectMany(od => od.Food.Restaurant.Branches).ToList();
                var random = new Random();
                var randomBranch = allBranches[random.Next(allBranches.Count)];

                // Ensure branch is tracked
                var branch = await branchRepository.GetAsync(randomBranch.Id);
                if (branch != null)
                    order.Branch = branch;
                else
                    return Result<bool>.Failure("Branch not found.", 400);
            }
            else
            {
                return Result<bool>.Failure("No branches available to assign to the order.", 400);
            }

            // Save order
            await orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"An error occurred while creating the order: {ex.Message}", 500);
        }
    }

    public async Task<Result<IEnumerable<OrderOutputDto>>> GetOredersByOwner(Guid ownerId)
    {
        try
        {
            // Получение ресторанов владельца
            var restaurantRepository = _unitOfWork.GetRepository<Restaurant>();
            var ownerRestaurants = await restaurantRepository.GetAsync(x => x.Owner.Id == ownerId);

            if (ownerRestaurants == null || !ownerRestaurants.Any())
            {
                return Result<IEnumerable<OrderOutputDto>>.Failure("No restaurants found for the given owner.", 404);
            }

            // Получение идентификаторов ресторанов владельца
            var restaurantIds = ownerRestaurants.Select(r => r.Id).ToList();

            // Получение заказов, связанных с этими ресторанами
            var orderRepository = _unitOfWork.GetRepository<Order>();
            var orders = await orderRepository.GetAsync(
                o => o.OrderDetails.Any(od => restaurantIds.Contains(od.Food.Restaurant.Id)),
                includeProperties: new[] { "Branch", "OrderDetails.Food" }
            );

            if (orders == null || !orders.Any())
            {
                return Result<IEnumerable<OrderOutputDto>>.Failure("No orders found for the given owner.", 404);
            }

            // Маппинг заказов в DTO
            var orderOutputDtos = _mapper.Map<IEnumerable<OrderOutputDto>>(orders);

            return Result<IEnumerable<OrderOutputDto>>.Success(orderOutputDtos);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<OrderOutputDto>>.Failure($"An error occurred while retrieving the orders: {ex.Message}", 500);
        }  
    }
}