using RestoRise.Domain.Common;

namespace RestoRise.Domain.Entities;

public class Restaurant:Entity
{
    public string Name { get; set; }
    public string Photo { get; set; }
    public string Description { get; set; }
    public User Owner { get; set; }
    public Menu? Menu { get; set; }
    public ICollection<Branch> Branches { get; set; }
    
    
}