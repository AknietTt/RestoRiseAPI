﻿namespace RestoRise.Application.DTOs.Restaurant;

public class RestaurantUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Photo { get; set; }
    public string Description { get; set; }

}