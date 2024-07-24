using Microsoft.EntityFrameworkCore;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage;

public interface IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Staff> Staves { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Food> Foods { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Role> Roles { get; set; }
}