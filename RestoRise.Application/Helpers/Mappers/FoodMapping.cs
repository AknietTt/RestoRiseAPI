using AutoMapper;
using RestoRise.Application.DTOs.Foods;
using RestoRise.Domain.Entities;

namespace RestoRise.Application.Helpers.Mappers;

public class FoodMapping:Profile
{
    public FoodMapping()
    {
        CreateMap<Food, Food>();
        CreateMap<Food, FoodUpdateDto>()
            .ForMember(dest=>dest.Category , opt=>opt.MapFrom(src=>src.Category.Name))
            .ReverseMap();
        CreateMap<Food, FoodOutputDto>().ReverseMap();
    }
}