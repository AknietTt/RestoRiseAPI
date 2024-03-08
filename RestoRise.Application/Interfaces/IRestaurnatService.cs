using RestoRise.BuisnessLogic.DTOs;
using RestoRise.Domain.Common;

namespace RestoRise.BuisnessLogic.Interfaces;

public interface IRestaurnatService
{
    Task<Result<IEnumerable<RestaurantOutputDto>>> GetAllRestaurants();
    Task<Result<IEnumerable<RestaurantOutputDto>>> GetAllRestaurants(Guid id);
    Task<Result<Guid>> CreateRestaurant(RestaurantCreateDto restaurantCreateDto);
    Task<Result<RestaurnatUpdateDto>> UpdateRestaurant(RestaurnatUpdateDto restaurnatUpdateDto);
    Task<Result<bool>> Delete(Guid id);
}