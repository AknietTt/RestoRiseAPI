namespace RestoRise.Application.DTOs.Branch;

public class BranchUpdateDto
{
    public Guid Id { get; set; }
    public string Address { get;  set; } = string.Empty;
    public Guid CityId { get; set; }
    public Guid RestaurantId { get; set; }

}