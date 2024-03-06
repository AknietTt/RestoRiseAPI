using AutoMapper;
using RestoRise.BuisnessLogic.DTOs.City;
using RestoRise.BuisnessLogic.ICrudRepository;
using RestoRise.BuisnessLogic.Interfaces;
using RestoRise.Domain.Common;
using RestoRise.Domain.Entities;

namespace RestoRise.BuisnessLogic.Services;

public class CityService: ICityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CityService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CityDto>>> GetAllCity()
    {
       var cityRepository = _unitOfWork.GetRepository<City>();
       var cities =  await cityRepository.GetAsync();
       return Result<IEnumerable<CityDto>>.Success(_mapper.Map<IEnumerable<CityDto>>(cities));
    }

    public async Task<Result<Guid>> CreateCity(string name)
    {
        var cityRepository = _unitOfWork.GetRepository<City>();
        var city = new City { Name = name };
        await cityRepository.AddAsync(city);
        await _unitOfWork.SaveChangesAsync();
        return Result<Guid>.Success(city.Id);
    }

    public async Task<Result<bool>> DeleteCity(Guid cityId)
    {
        try
        {
            var cityRepository = _unitOfWork.GetRepository<City>();
            await cityRepository.Delete(cityId);
            await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
        catch (Exception e)
        {
            return Result<bool>.Failure("Ошибка при удалений города, может быть что нет такой id или не правильные данные", 400);
        }
    }

    public async Task<Result<CityDto>> UpdateCity(CityDto cityUpdateDto)
    {
        var cityRepository = _unitOfWork.GetRepository<City>();
        var city = await cityRepository.GetAsync(cityUpdateDto.Id);

        city.Name = cityUpdateDto.Name;
        
        cityRepository.Update(city);
        var res =  await _unitOfWork.SaveChangesAsync();
        if (res != 0)
        {
            return Result<CityDto>.Success(cityUpdateDto);    
        }

        return Result<CityDto>.Failure("Ошибка при сохранений", 400);

    }
}