using AutoMapper;
using RestoRise.BuisnessLogic.DTOs.City;
using RestoRise.Domain.Entities;

namespace RestoRise.BuisnessLogic.Helpers.Mappers;

public class CityMapping: Profile
{
    public CityMapping()
    {
        CreateMap<City, CityDto>().ReverseMap();
    }
}