using RestoRise.Domain.Common;

namespace RestoRise.Domain.Entities;

public class City:Entity
{
    public string Name { get; set; }
    public ICollection<Branch> Branches { get; set; }   
}