using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Configurations;

public class RoleConfiguration:IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .HasMany(s => s.Staves)
            .WithMany(s => s.Roles);
    }
}