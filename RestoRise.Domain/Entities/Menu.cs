using RestoRise.Domain.Common;

namespace RestoRise.Domain.Entities;

public class Menu:Entity
{
    public ICollection<Restaurant> Restaurants { get; set; }
    public ICollection<Food> Foods { get; set; }
}