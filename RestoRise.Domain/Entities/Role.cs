using RestoRise.Domain.Common;

namespace RestoRise.Domain.Entities;

public class Role:Entity
{
    public string Name { get; set; }
    public ICollection<Staff> Staves { get; set; }
    
}