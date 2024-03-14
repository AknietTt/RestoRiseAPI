using RestoRise.Application.DTOs.City;
using RestoRise.Domain.Common;

namespace RestoRise.Application.Interfaces.Services;
public interface ICityService
{
    Task<Result<IEnumerable<CityDto>>> GetAllCity();
    Task<Result<Guid>> CreateCity(string name);
    Task<Result<bool>> DeleteCity(Guid cityId);
    Task<Result<CityDto>> UpdateCity(CityDto cityUpdateDto);
}