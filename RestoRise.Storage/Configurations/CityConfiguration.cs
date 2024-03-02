using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestoRise.Domain.Entities;


namespace RestoRise.Storage.Configurations;

public class CityConfiguration:IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        
        builder
            .HasMany(b => b.Branches)
            .WithOne(c => c.City);
        
    }
}