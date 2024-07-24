using RestoRise.Application.DTOs.Restaurant;
using RestoRise.Domain.Common;

namespace RestoRise.Application.Interfaces.Services;
public interface IRestaurnatService
{
    Task<Result<IEnumerable<RestaurantOutputDto>>> GetAllRestaurants();
    Task<Result<IEnumerable<RestaurantOutputDto>>> GetAllRestaurants(Guid id);
    Task<Result<Guid>> CreateRestaurant(RestaurantCreateDto restaurantCreateDto);
    Task<Result<RestaurantUpdateDto>> UpdateRestaurant(RestaurantUpdateDto restaurnatUpdateDto);
    Task<Result<bool>> Delete(Guid id);
    Task<Result<IEnumerable<RestaurantOutputDto>>> GetRestaurantsByOwner(Guid ownerId);
    Task<Result<RestaurantUpdateDto>> GetRestaurantById(Guid id);
}