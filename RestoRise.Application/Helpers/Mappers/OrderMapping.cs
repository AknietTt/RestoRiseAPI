using AutoMapper;
using RestoRise.Application.DTOs.Order;
using RestoRise.Domain.Entities;

namespace RestoRise.Application.Helpers.Mappers;

public class OrderMapping:Profile
{
    public OrderMapping()
    {
        CreateMap<Order, AddOrderDto>().ReverseMap();
        
        CreateMap<Order, OrderOutputDto>()
            .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch.Address))
            .ForMember(dest => dest.RestaurantName, opt => opt.MapFrom(src => src.OrderDetails.First().Food.Restaurant.Name));
        
        CreateMap<OrderDetail, OrderDetailOutputDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Food.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Food.Price))
            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Food.Photo))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Food.Description))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Food.Category.Name))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count));

        CreateMap<Order, Order>();
    }
}