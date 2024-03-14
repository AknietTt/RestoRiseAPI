namespace RestoRise.Application.DTOs.Branch;

public class BranchCreateDto
{
    public string Address { get;  set; }
    public Guid CityId { get; set; }
    
    public Guid RestaurantId { get; set; }

}