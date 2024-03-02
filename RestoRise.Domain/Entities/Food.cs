using RestoRise.Domain.Common;

namespace RestoRise.Domain.Entities;

public class Food:Entity
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Photo { get; set; }
    
    public Menu Menu { get; set; }
    public Category Category { get; set; }
    public IEnumerable<OrderDetail> OrderDetails { get; set; }
}