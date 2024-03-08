using AutoMapper;
using RestoRise.BuisnessLogic.DTOs;
using RestoRise.Domain.Entities;

namespace RestoRise.BuisnessLogic.Helpers.Mappers;

public class RestaurantMapping: Profile
{
    public RestaurantMapping()
    {
        CreateMap<Restaurant, RestaurantCreateDto>().ReverseMap();
        CreateMap<Restaurant, RestaurantOutputDto>().ReverseMap();
    }
}