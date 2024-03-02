using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Configurations;

public class StaffConfiguration:IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        
        builder
            .HasMany(x => x.Branches)
            .WithMany(b => b.Staves)
            .UsingEntity(j=>j.ToTable("BranchStaves"));

        builder
            .HasMany(c => c.Roles)
            .WithMany(r => r.Staves)
            .UsingEntity(j=>j.ToTable("RoleStaves"));
        
    }
}