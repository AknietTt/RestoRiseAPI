using RestoRise.Domain.Common;

namespace RestoRise.Domain.Entities;

public class Order:Entity
{
    public DateTimeOffset CreateAt { get; set; }
    public double Summa { get; set; }
    public string Address { get; set; }
    public string NameCustomer { get; set; }
    public string PhoneNumber { get; set; }
    public string Entrance { get; set; }
    public string Intercom { get; set; }
    public string Comment { get; set; }
    public Branch Branch { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}