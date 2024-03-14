using AutoMapper;
using RestoRise.Application.DTOs.Restaurant;
using RestoRise.Domain.Entities;

namespace RestoRise.Application.Helpers.Mappers;

public class RestaurantMapping: Profile
{
    public RestaurantMapping()
    {
        CreateMap<Restaurant, Restaurant>()
            .ForMember(dest => dest.Menu, opt => opt.MapFrom(src => src.Menu));
        
        CreateMap<Restaurant, RestaurantCreateDto>().ReverseMap();
        CreateMap<Restaurant, RestaurantOutputDto>()
            .ForMember(dest => dest.MenuId, opt => opt.MapFrom(src => src.Menu.Id));
    }
}