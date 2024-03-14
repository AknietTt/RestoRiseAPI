using AutoMapper;
using RestoRise.Domain.Entities;

namespace RestoRise.Application.Helpers.Mappers;

public class StaffMapping:Profile
{
    public StaffMapping()
    {
        CreateMap<Staff, Staff>();
    }
}