
using RestoRise.Domain.Entities;

namespace RestoRise.Application.Interfaces.Repositories;

public interface IRestaurantRepositry:ICrudRepository<Restaurant>
{
    Task<ICollection<Restaurant>> GetRestaurantsByCity(Guid cityId);
}