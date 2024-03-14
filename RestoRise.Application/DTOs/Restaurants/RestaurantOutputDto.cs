namespace RestoRise.Application.DTOs.Restaurant;

public class RestaurantOutputDto
{
    public Guid Id { get; set; }
    public string Name { get;  set; }
    public string Photo { get; set; }
    public string Description { get; set; }
    public Guid MenuId { get; set; }
}