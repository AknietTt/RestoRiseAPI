namespace RestoRise.Application.DTOs.Foods;

public class FoodCreateDto
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Photo { get; set; }
    
    public Guid MenuId { get; set; }
    public string Category { get; set; }
}