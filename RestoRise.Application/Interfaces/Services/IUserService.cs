
using RestoRise.Application.DTOs;
using RestoRise.Domain.Common;

namespace RestoRise.Application.Interfaces.Services;

public interface IUserService
{
    Task<Result<Guid>> Register(UserCreateDto user);
    Task<Result<object>> Authorize(LoginDto loginDto);
    Task<Result<UserDto>> GetUserInfo(Guid userId);
}