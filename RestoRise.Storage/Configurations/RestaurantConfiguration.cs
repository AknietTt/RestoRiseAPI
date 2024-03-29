﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Configurations;

public class RestaurantConfiguration:IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {

        builder
            .HasOne(m => m.Menu)
            .WithMany(r => r.Restaurants);
        
           builder
               .HasOne(o => o.Owner)
               .WithMany(r => r.Restaurants);

           builder
               .HasMany(b => b.Branches)
               .WithOne(r => r.Restaurant);

    }
}