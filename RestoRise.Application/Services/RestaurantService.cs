using AutoMapper;
using RestoRise.BuisnessLogic.DTOs;
using RestoRise.BuisnessLogic.ICrudRepository;
using RestoRise.BuisnessLogic.Interfaces;
using RestoRise.Domain.Common;
using RestoRise.Domain.Entities;

namespace RestoRise.BuisnessLogic.Services;

public class RestaurantService:IRestaurnatService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public RestaurantService(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userRepository = userRepository;
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

    public Task<Result<IEnumerable<RestaurantOutputDto>>> GetAllRestaurants(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Guid>> CreateRestaurant(RestaurantCreateDto restaurantCreateDto)
    {
        try
        {
            var restaurantRepository = _unitOfWork.GetRepository<Restaurant>();
            var userRepository = _unitOfWork.GetRepository<User>();
            var owner = await userRepository.FirstOrDefault(u => u.Id == restaurantCreateDto.OwnerId);
        
            if (owner == null)
            {
                return Result<Guid>.Failure("Пользователь не найден", 404);
            }
            
            var restaurant = _mapper.Map<Restaurant>(restaurantCreateDto);
            
            userRepository.Attach(owner);
            restaurant.Owner = owner;
            
            await restaurantRepository.AddAsync(restaurant);
            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Success(restaurant.Id, 200);
        }
        catch (Exception e)
        {
            return Result<Guid>.Failure(error: e.Message, 400);
        }
    }

    public async Task<Result<RestaurnatUpdateDto>> UpdateRestaurant(RestaurnatUpdateDto restaurnatUpdateDto)
    {
        try
        {
            var restaurantRepository = _unitOfWork.GetRepository<Restaurant>();
            var restaurant = await restaurantRepository.GetAsync(restaurnatUpdateDto.Id);
        
            restaurant.Name = restaurnatUpdateDto.Name;
            restaurant.Description = restaurnatUpdateDto.Description;
            restaurant.Photo = restaurnatUpdateDto.Photo;
        
            restaurantRepository.Update(restaurant);
            await _unitOfWork.SaveChangesAsync();
            return Result<RestaurnatUpdateDto>.Success(restaurnatUpdateDto, 201);
        }
        catch (Exception e)
        {
            return Result<RestaurnatUpdateDto>.Failure(error: e.Message, 400);

        }
        
    }

    public async Task<Result<bool>> Delete(Guid id)
    {
        try
        {
          var restaurantRepository =  _unitOfWork.GetRepository<Restaurant>();
          await restaurantRepository.Delete(id);
          await _unitOfWork.SaveChangesAsync();
          return Result<bool>.Success(true);
        }
        catch (Exception e)
        {
            return Result<bool>.Failure(error: e.Message, 400);
        }
    }
}