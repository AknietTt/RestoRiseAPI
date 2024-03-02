
using RestoRise.BuisnessLogic.DTOs;
using RestoRise.Domain.Common;

namespace RestoRise.BuisnessLogic.Interfaces;

public interface IUserService
{
    Task<Result<Guid>> Register(UserCreateDto user);
}