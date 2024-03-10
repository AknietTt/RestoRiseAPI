using AutoMapper;
using RestoRise.Domain.Entities;

namespace RestoRise.BuisnessLogic.Helpers.Mappers;

public class StaffMapping:Profile
{
    public StaffMapping()
    {
        CreateMap<Staff, Staff>();
    }
}