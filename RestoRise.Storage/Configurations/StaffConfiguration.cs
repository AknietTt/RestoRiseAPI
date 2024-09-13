using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Configurations;

public class StaffConfiguration:IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {

        builder
            .HasOne(x => x.Branch)
            .WithMany(b => b.Staves);

        builder
            .HasMany(c => c.Roles)
            .WithMany(r => r.Staves)
            .UsingEntity(j=>j.ToTable("RoleStaves"));

        builder.Property(bd => bd.DateOfBirthDate).HasColumnType("timestamp without time zone");
    }
}