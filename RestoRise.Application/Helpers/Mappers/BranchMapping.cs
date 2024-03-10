using AutoMapper;
using RestoRise.BuisnessLogic.DTOs.Branch;
using RestoRise.Domain.Entities;

namespace RestoRise.BuisnessLogic.Helpers.Mappers;

public class BranchMapping:Profile
{
    public BranchMapping()
    { 
        //CreateMap<Branch, Branch>();
        CreateMap<Branch, Branch>()
            .ForMember(dest => dest.Staves, opt => opt.MapFrom(src => src.Staves));
        CreateMap<BranchCreateDto, Branch>().ReverseMap();
        CreateMap<Branch, BranchOutputDto>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name))
            .ForMember(dest => dest.Restaurant, opt => opt.MapFrom(src => src.Restaurant.Name));
    }
}