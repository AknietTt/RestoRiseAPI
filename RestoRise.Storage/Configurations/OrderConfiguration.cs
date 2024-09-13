using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Configurations;

public class OrderConfiguration:IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        
        builder
            .HasOne(b => b.Branch)
            .WithMany(c => c.Orders);
        
        builder
            .HasMany(x => x.OrderDetails)
            .WithOne(x => x.Order);
        
        builder
            .Property(o => o.Status)
            .HasConversion<string>();
    }
}