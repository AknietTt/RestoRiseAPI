using AutoMapper;
using RestoRise.Application.DTOs.Foods;
using RestoRise.Domain.Entities;

namespace RestoRise.Application.Helpers.Mappers;

public class FoodMapping:Profile
{
    public FoodMapping()
    {
        CreateMap<Food, FoodOutputDto>().ReverseMap();
    }
}