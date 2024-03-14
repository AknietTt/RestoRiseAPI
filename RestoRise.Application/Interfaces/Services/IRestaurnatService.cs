using RestoRise.Application.DTOs.Restaurant;
using RestoRise.Domain.Common;

namespace RestoRise.Application.Interfaces.Services;
public interface IRestaurnatService
{
    Task<Result<IEnumerable<RestaurantOutputDto>>> GetAllRestaurants();
    Task<Result<IEnumerable<RestaurantOutputDto>>> GetAllRestaurants(Guid id);
    Task<Result<Guid>> CreateRestaurant(RestaurantCreateDto restaurantCreateDto);
    Task<Result<RestaurnatUpdateDto>> UpdateRestaurant(RestaurnatUpdateDto restaurnatUpdateDto);
    Task<Result<bool>> Delete(Guid id);
}