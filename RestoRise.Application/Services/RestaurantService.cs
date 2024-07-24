using AutoMapper;
using RestoRise.Application.DTOs.Restaurant;
using RestoRise.Application.Interfaces.Repositories;
using RestoRise.Application.Interfaces.Services;
using RestoRise.Domain.Common;
using RestoRise.Domain.Entities;

namespace RestoRise.BuisnessLogic.Services;

public class RestaurantService : IRestaurnatService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IRestaurantRepositry _restaurantRepositry;
    private readonly IMapper _mapper;

    public RestaurantService(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository, IRestaurantRepositry restaurantRepositry)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userRepository = userRepository;
        _restaurantRepositry = restaurantRepositry;
    }

    public async Task<Result<IEnumerable<RestaurantOutputDto>>> GetAllRestaurants()
    {
        var restaurantRepositry = _unitOfWork.GetRepository<Restaurant>();
        var restaurnts = await restaurantRepositry.GetAsync(includeProperties:new [] {"Menu"} );

        var result = _mapper.Map<IEnumerable<RestaurantOutputDto>>(restaurnts);

        return Result<IEnumerable<RestaurantOutputDto>>.Success(result, 200);
    }

    public async Task<Result<IEnumerable<RestaurantOutputDto>>> GetAllRestaurants(Guid id)
    {
       var restaurants =  _mapper.Map<IEnumerable<RestaurantOutputDto>>(await _restaurantRepositry.GetRestaurantsByCity(id));
       return Result<IEnumerable<RestaurantOutputDto>>.Success(restaurants);
    }

    public async Task<Result<Guid>> CreateRestaurant(RestaurantCreateDto restaurantCreateDto)
    {
        var restaurantRepository = _unitOfWork.GetRepository<Restaurant>();
        var userRepository = _unitOfWork.GetRepository<User>();
        var owner = await userRepository.FirstOrDefault(u => u.Id == restaurantCreateDto.OwnerId);

        if (owner == null) return Result<Guid>.Failure("Пользователь не найден", 404);

        var restaurant = _mapper.Map<Restaurant>(restaurantCreateDto);
        
        
        userRepository.Attach(owner);
        restaurant.Owner = owner;

        await restaurantRepository.AddAsync(restaurant);
        await _unitOfWork.SaveChangesAsync();

        return Result<Guid>.Success(restaurant.Id, 201);
    }

    public async Task<Result<RestaurantUpdateDto>> UpdateRestaurant(RestaurantUpdateDto restaurnatUpdateDto)
    {
        var restaurantRepository = _unitOfWork.GetRepository<Restaurant>();
        var restaurant = await restaurantRepository.GetAsync(restaurnatUpdateDto.Id);

        restaurant.Name = restaurnatUpdateDto.Name;
        restaurant.Description = restaurnatUpdateDto.Description;
        restaurant.Photo = restaurnatUpdateDto.Photo;

        restaurantRepository.Update(restaurant);
        await _unitOfWork.SaveChangesAsync();
        return Result<RestaurantUpdateDto>.Success(restaurnatUpdateDto, 200);
    }

    public async Task<Result<bool>> Delete(Guid id)
    {
        var restaurantRepository = _unitOfWork.GetRepository<Restaurant>();
        if ( !await restaurantRepository.Delete(id))
        {
            return Result<bool>.Failure("Ошибка при удалений возможно не правильный id", 400);
        }

        await _unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<RestaurantOutputDto>>> GetRestaurantsByOwner(Guid ownerId)
    {
        var restaurants =   _mapper.Map<IEnumerable<RestaurantOutputDto>>(await _restaurantRepositry.GetAsync(x=>x.Owner.Id == ownerId));
        return Result<IEnumerable<RestaurantOutputDto>>.Success(restaurants);
    }

    public async Task<Result<RestaurantUpdateDto>> GetRestaurantById(Guid id)
    {
      var restaurant =  await _restaurantRepositry.FirstOrDefault(x => x.Id == id);
      return Result<RestaurantUpdateDto>.Success( _mapper.Map<RestaurantUpdateDto>(restaurant), 200);
    }
}