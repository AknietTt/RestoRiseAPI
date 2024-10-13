namespace RestoRise.Application.DTOs.Order;

public class AddOrderDto
{
    public double Summa { get; set; }
    public string Address { get; set; }
    public string NameCustomer { get; set; }
    public string PhoneNumber { get; set; }
    public string Entrance { get; set; }
    public string Intercom { get; set; }
    public string Comment { get; set; }
    public Guid BranchId { get; set; }
    public ICollection<OrderDetailDto> OrderDetailDtos { get; set; }
}