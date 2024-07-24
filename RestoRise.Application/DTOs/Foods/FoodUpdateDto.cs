namespace RestoRise.Application.DTOs.Foods;

public class FoodUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Photo { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }

}