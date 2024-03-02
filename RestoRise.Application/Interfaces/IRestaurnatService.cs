using RestoRise.BuisnessLogic.DTOs;
using RestoRise.Domain.Common;

namespace RestoRise.BuisnessLogic.Interfaces;

public interface IRestaurnatService
{
    Task<Result<IEnumerable<RestaurantOutputDto>>> GetAllRestaurants();
    Task<Result<Guid>> CreateRestaurant(CreateRestaurantDto createRestaurantDto);
}