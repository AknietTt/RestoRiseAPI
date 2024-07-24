namespace RestoRise.Application.DTOs.Foods;

public class FoodOutputDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Photo { get; set; }
    public string Description { get; set; }

}

public class MenuOutputDto
{
    public string CategoryName { get; set; }
    public ICollection<FoodOutputDto> Foods { get; set; }
}