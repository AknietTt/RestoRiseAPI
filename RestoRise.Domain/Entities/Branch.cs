using RestoRise.Domain.Common;

namespace RestoRise.Domain.Entities;

public class Branch:Entity
{
    public string Address { get;  set; } = string.Empty;
    public City City { get; set; }
    
    public Restaurant Restaurant { get; set; }
    public ICollection<Staff> Staves { get; set; }
    public ICollection<Order> Orders { get; set; }
}