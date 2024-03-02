using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Configurations;

public class BranchConfiguration:IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        
        builder
            .HasOne(r => r.Restaurant)
            .WithMany(b => b.Branches);
        builder
            .HasOne(c => c.City)
            .WithMany(b => b.Branches);

    }
}