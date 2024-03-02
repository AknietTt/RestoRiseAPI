using RestoRise.Domain.Common;

namespace RestoRise.Domain.Entities;

public class Branch:Entity
{
    public string Address { get;  set; } = string.Empty;
    public City City { get; set; }
    
    public Restaurant Restaurant { get; set; }
    public IEnumerable<Staff> Staves { get; set; }
    public IEnumerable<Order> Orders { get; set; }
}