﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Configurations;

public class MenuConfiguration:IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        
        builder
            .HasMany(r => r.Restaurants)
            .WithOne(m => m.Menu);

        builder
            .HasMany(f => f.Foods)
            .WithOne(m => m.Menu);
    }
}