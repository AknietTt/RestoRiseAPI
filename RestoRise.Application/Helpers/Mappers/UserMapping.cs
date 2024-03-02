using AutoMapper;
using RestoRise.BuisnessLogic.DTOs;
using RestoRise.Domain.Entities;

namespace RestoRise.BuisnessLogic.Helpers.Mappers;

public class UserMapping:Profile
{
    public UserMapping()
    {
        CreateMap<User, UserCreateDto>().ReverseMap();
    }
}