using AutoMapper;
using RestoRise.BuisnessLogic.DTOs;
using RestoRise.BuisnessLogic.ICrudRepository;
using RestoRise.BuisnessLogic.Interfaces;
using RestoRise.Domain.Common;
using RestoRise.Domain.Entities;

namespace RestoRise.BuisnessLogic.Services;

public class RestaurantService:IRestaurnatService
{
   // private readonly IRestaurantRepositry _restaurantRepositry;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RestaurantService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        //   _restaurantRepositry = restaurantRepositry;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<RestaurantOutputDto>>> GetAllRestaurants()
    {
        try
        {
            var restaurantRepositry = _unitOfWork.GetRepository<Restaurant>();
            var restaurnts = await restaurantRepositry.GetAsync();
        
            var result = _mapper.Map<IEnumerable<RestaurantOutputDto>>(restaurnts);

            return Result<IEnumerable<RestaurantOutputDto>>.Success(result , 200);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<RestaurantOutputDto>>.Failure(ex.Message , 500);
        }
    }

    public async Task<Result<Guid>> CreateRestaurant(CreateRestaurantDto createRestaurantDto)
    {
        try
        {
            var restaurantRepositry= _unitOfWork.GetRepository<Restaurant>();
            var restaurant = _mapper.Map<Restaurant>(createRestaurantDto);
            await restaurantRepositry.AddAsync(restaurant);
            return Result<Guid>.Success(  restaurant.Id , 200);
        }
        catch (Exception e)
        {
            return Result<Guid>.Failure(error: e.Message, 400);
        }
       
    }
}