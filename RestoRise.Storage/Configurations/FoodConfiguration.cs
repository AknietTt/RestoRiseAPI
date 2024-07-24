using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestoRise.Domain.Entities;


namespace RestoRise.Storage.Configurations;

public class FoodConfiguration:IEntityTypeConfiguration<Food>
{
    public void Configure(EntityTypeBuilder<Food> builder)
    {
        
        builder
            .HasOne(c => c.Category)
            .WithMany(f => f.Foods);

        builder.HasOne(r => r.Restaurant)
            .WithMany(r => r.Foods);
    }
}