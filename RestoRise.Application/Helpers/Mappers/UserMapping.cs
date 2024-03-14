using AutoMapper;
using RestoRise.Application.DTOs;
using RestoRise.Domain.Entities;

namespace RestoRise.Application.Helpers.Mappers;

public class UserMapping:Profile
{
    public UserMapping()
    {
        CreateMap<User, UserCreateDto>().ReverseMap();
    }
}