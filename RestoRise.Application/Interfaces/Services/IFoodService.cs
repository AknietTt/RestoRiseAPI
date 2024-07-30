using RestoRise.Application.DTOs.Foods;
using RestoRise.Domain.Common;

namespace RestoRise.Application.Interfaces.Services;

public interface IFoodService
{
    Task<Result<FoodCreateDto>> CraeteFood(FoodCreateDto dto);
    Task<Result<IEnumerable<MenuOutputDto>>> GetFoodsByRestaurant(Guid restaurnatId);
    Task<Result<bool>> DeleteFood(Guid id);
    Task<Result<FoodUpdateDto>> UpdateFood(FoodUpdateDto dto);
    Task<Result<FoodUpdateDto>> GetFoodById(Guid id);
}