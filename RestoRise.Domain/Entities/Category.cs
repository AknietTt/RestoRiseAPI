using RestoRise.Domain.Common;

namespace RestoRise.Domain.Entities;

public class Category:Entity
{
    public string Name { get; set; }
    public IEnumerable<Food> Foods { get; set; }    
    
}