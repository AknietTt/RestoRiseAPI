
using RestoRise.Application.DTOs;
using RestoRise.Domain.Common;

namespace RestoRise.Application.Interfaces.Services;

public interface IUserService
{
    Task<Result<Guid>> Register(UserCreateDto user);
    Task<Result<string>> Authorize(LoginDto loginDto);
}