using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Configurations;

public class OrderDetailConfiguration:IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        
        builder
            .HasOne(o => o.Order)
            .WithMany(o => o.OrderDetails);

        builder
            .HasOne(f => f.Food)
            .WithMany(o => o.OrderDetails);
        
    }
}