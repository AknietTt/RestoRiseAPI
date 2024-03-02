using RestoRise.Domain.Common;

namespace RestoRise.Domain.Entities;

public class OrderDetail:Entity
{
    public Order Order { get; set; }
    public Food Food { get; set; }
    public int Count { get; set; }
    
}