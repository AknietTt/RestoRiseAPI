using RestoRise.BuisnessLogic.DTOs.City;
using RestoRise.Domain.Common;

namespace RestoRise.BuisnessLogic.Interfaces;

public interface ICityService
{
    Task<Result<IEnumerable<CityDto>>> GetAllCity();
    Task<Result<Guid>> CreateCity(string name);
    Task<Result<bool>> DeleteCity(Guid cityId);
    Task<Result<CityDto>> UpdateCity(CityDto cityUpdateDto);
}