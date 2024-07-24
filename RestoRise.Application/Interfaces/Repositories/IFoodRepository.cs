using RestoRise.Domain.Entities;

namespace RestoRise.Application.Interfaces.Repositories;

public interface IFoodRepository:ICrudRepository<Food>
{
    Task<ICollection<Food>> GetFoodsByRestaurantId(Guid restaurantId);
}