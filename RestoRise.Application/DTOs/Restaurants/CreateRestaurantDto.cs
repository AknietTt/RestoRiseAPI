namespace RestoRise.BuisnessLogic.DTOs;

public class CreateRestaurantDto
{
    public string Name { get; set; }
    public string Photo { get; set; }
    public string Description { get; set; }
    public Guid OwnerId { get; set; }
}