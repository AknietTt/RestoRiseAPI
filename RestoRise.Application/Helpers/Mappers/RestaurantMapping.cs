using AutoMapper;
using RestoRise.Application.DTOs.Restaurant;
using RestoRise.Domain.Entities;

namespace RestoRise.Application.Helpers.Mappers;

public class RestaurantMapping: Profile
{
    public RestaurantMapping()
    {
        CreateMap<Restaurant, Restaurant>();
        CreateMap<Restaurant, RestaurantUpdateDto>().ReverseMap();
        CreateMap<Restaurant, RestaurantCreateDto>().ReverseMap();
        CreateMap<Restaurant, RestaurantOutputDto>();
    }
}