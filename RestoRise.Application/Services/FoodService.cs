using AutoMapper;
using RestoRise.Application.DTOs.Foods;
using RestoRise.Application.Interfaces.Repositories;
using RestoRise.Application.Interfaces.Services;
using RestoRise.Domain.Common;
using RestoRise.Domain.Entities;

namespace RestoRise.BuisnessLogic.Services;

public class FoodService : IFoodService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FoodService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<FoodCreateDto>> CraeteFood(FoodCreateDto dto)
    {
        var foodRepository = _unitOfWork.GetRepository<Food>();
        var categoryRepository = _unitOfWork.GetRepository<Category>();
        var restaurantRepository = _unitOfWork.GetRepository<Restaurant>();

        var categoryName = dto.Category.Trim();
        var category = await categoryRepository.FirstOrDefault(x => x.Name == categoryName);

        if (category == null)
        {
            category = new Category { Name = categoryName };
            await categoryRepository.AddAsync(category);
        }
        else
        {
            categoryRepository.Attach(category);
        }

        var restaurant = await restaurantRepository.FirstOrDefault(x => x.Id == dto.RestaurantId);
        restaurantRepository.Attach(restaurant);
        var food = new Food
        {
            Name = dto.Name,
            Photo = dto.Photo,
            Price = dto.Price,
            Restaurant = restaurant,
            Category = category,
            Description = dto.Description
        };

        await foodRepository.AddAsync(food);
        await _unitOfWork.SaveChangesAsync();

        return Result<FoodCreateDto>.Success(dto, 201);
    }

    public async Task<Result<IEnumerable<MenuOutputDto>>> GetFoodsByRestaurant(Guid restaurnatId)
    {
        if (!await _unitOfWork.GetRepository<Restaurant>().Any(x => x.Id == restaurnatId))
            return Result<IEnumerable<MenuOutputDto>>.Failure("Ресторан не найден, такого ресторана нет", 404);
    
        var foods = await _unitOfWork.GetRepository<Food>()
            .GetAsync(
                x => x.Restaurant.Id == restaurnatId, 
                x => x.OrderBy(o => o.Category),
                includeProperties:new [] {"Category"}
                );

        var groupedFoods = foods.GroupBy(f => f.Category.Name);

        var menus = new List<MenuOutputDto>();

        foreach (var group in groupedFoods)
        {
            var menu = new MenuOutputDto
            {
                CategoryName = group.Key,
                Foods = _mapper.Map<ICollection<FoodOutputDto>>(group)
            };
            menus.Add(menu);
        }

        return Result<IEnumerable<MenuOutputDto>>.Success(menus, 200);
    }


    public async Task<Result<bool>> DeleteFood(Guid id)
    {
        var foodRepository = _unitOfWork.GetRepository<Food>();
        if (!await foodRepository.Any(x => x.Id == id))
            return Result<bool>.Failure("Такой еды нет , не правильный id", 404);
        await foodRepository.Delete(id);
        _unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }

    public async Task<Result<FoodUpdateDto>> UpdateFood(FoodUpdateDto dto)
    {
        var foodRepository = _unitOfWork.GetRepository<Food>();
        var categoryRepository = _unitOfWork.GetRepository<Category>();

        var food = await foodRepository.GetAsync(dto.Id);

        if (food == null) return Result<FoodUpdateDto>.Failure("Такой еды нет , ошибка возможно нет такой Id", 404);
        var category = await categoryRepository.FirstOrDefault(c => c.Name == dto.Category);

        if (category != null) categoryRepository.Attach(category);
        if (category == null)
        {
            category = new Category { Name = dto.Category };
            await categoryRepository.AddAsync(category);
        }

        food.Category = category;
        food.Name = dto.Name;
        food.Price = dto.Price;
        food.Photo = dto.Photo;

        foodRepository.Update(food);

        await _unitOfWork.SaveChangesAsync();
        return Result<FoodUpdateDto>.Success(dto);
    }

    public async Task<Result<FoodUpdateDto>> GetFoodById(Guid id)
    {
       var food = await _unitOfWork.GetRepository<Food>().FirstOrDefault(x => x.Id == id, includeProperties:new [] {"Category"});
       return Result<FoodUpdateDto>.Success( _mapper.Map<FoodUpdateDto>(food));
    }
}