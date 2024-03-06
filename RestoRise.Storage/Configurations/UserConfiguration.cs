using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Configurations;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        
        builder
            .HasMany(r => r.Restaurants)
            .WithOne(r => r.Owner);

        builder.Property(bd => bd.DateOfBirthDate).HasColumnType("timestamp without time zone");
        
    }
}