using RestoRise.Domain.Common;

namespace RestoRise.Domain.Entities;

public class Menu:Entity
{
    public IEnumerable<Restaurant> Restaurants { get; set; }
    public IEnumerable<Food> Foods { get; set; }
}