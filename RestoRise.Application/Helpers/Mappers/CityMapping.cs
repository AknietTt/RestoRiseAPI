using AutoMapper;
using RestoRise.Application.DTOs.City;
using RestoRise.Domain.Entities;

namespace RestoRise.BuisnessLogic.Helpers.Mappers;

public class CityMapping: Profile
{
    public CityMapping()
    {
        CreateMap<City, CityDto>().ReverseMap();
    }
}