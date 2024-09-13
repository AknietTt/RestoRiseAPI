using RestoRise.Domain.Enums;

namespace RestoRise.Application.DTOs.Order;

public class OrderOutputDto
{
    public Guid Id { get; set; }
    public DateTimeOffset CreateAt { get; set; }
    public double Summa { get; set; }
    public string Address { get; set; }
    public string RestaurantName { get; set; }
    public string NameCustomer { get; set; }
    public string PhoneNumber { get; set; }
    public string Entrance { get; set; }
    public string Intercom { get; set; }
    public string Comment { get; set; }
    public string Branch { get; set; }
    public ICollection<OrderDetailOutputDto> OrderDetails { get; set; }
    public OrderStatus Status { get; set; } 
}

public class OrderDetailOutputDto
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Photo { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public int Count { get; set; }

}