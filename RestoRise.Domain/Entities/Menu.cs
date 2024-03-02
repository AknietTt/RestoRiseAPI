using RestoRise.Domain.Common;

namespace RestoRise.Domain.Entities;

public class Menu:Entity
{
    public Restaurant Restaurant { get; set; }
    public IEnumerable<Food> Foods { get; set; }
}