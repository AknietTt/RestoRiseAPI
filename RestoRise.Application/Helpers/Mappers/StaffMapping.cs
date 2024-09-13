using AutoMapper;
using RestoRise.Application.DTOs;
using RestoRise.Application.DTOs.Staff;
using RestoRise.Domain.Entities;

namespace RestoRise.Application.Helpers.Mappers;

public class StaffMapping:Profile
{
    public StaffMapping()
    {
        CreateMap<Staff, Staff>().ReverseMap();
        CreateMap<StaffRegisterDto, Staff>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore()) 
            .ForMember(dest => dest.Branch, opt => opt.Ignore());
        
        CreateMap<Staff, StaffOutputDto>()
            .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch.Address))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(role => role.Name)));

        CreateMap<Staff, StaffUpdateDto>()
            .ForMember(dest => dest.Password,
                opt => opt.MapFrom(
                    src => BCrypt.Net.BCrypt.HashPassword(src.Password, BCrypt.Net.BCrypt.GenerateSalt())))
            .ForMember(dest=>dest.Roles , opt=>opt.Ignore())
            .ForMember(dest=>dest.Branch , opt=>opt.Ignore())
            .ReverseMap();
        
        CreateMap<StaffUpdateDto, Staff>()
            .ForMember(dest => dest.Password,
                opt => opt.MapFrom(
                    src => BCrypt.Net.BCrypt.HashPassword(src.Password, BCrypt.Net.BCrypt.GenerateSalt())))

            .ForMember(dest => dest.Roles, opt => opt.Ignore())  // Игнорируем Roles, будем обрабатывать вручную
            .ForMember(dest => dest.Branch, opt => opt.Ignore());

        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<Staff , UserDto>().ReverseMap();

    }
}