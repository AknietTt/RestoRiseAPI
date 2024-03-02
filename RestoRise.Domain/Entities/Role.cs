using RestoRise.Domain.Common;

namespace RestoRise.Domain.Entities;

public class Role:Entity
{
    public string Name { get; set; }
    public IEnumerable<Staff> Staves { get; set; }
    
}