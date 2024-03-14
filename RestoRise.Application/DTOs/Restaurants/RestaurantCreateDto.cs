namespace RestoRise.Application.DTOs.Restaurant;

public class RestaurantCreateDto
{
    public string Name { get; set; }
    public string Photo { get; set; }
    public string Description { get; set; }
    public Guid OwnerId { get; set; }
}