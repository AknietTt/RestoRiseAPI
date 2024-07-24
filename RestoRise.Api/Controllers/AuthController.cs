using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RestoRise.Application.DTOs;
using RestoRise.Application.Interfaces.Services;

namespace RestoRise.Api.Controllers.Auth;

[ApiController]
[Route("auth")]
public class AuthController:ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("registration")]
    public async Task<IActionResult> Registration([FromBody] UserCreateDto registration)
    {
        var result = await _userService.Register(registration);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPost("athorize")]
    public async Task<IActionResult> Authorize([FromBody] LoginDto loginDto)
    {
        var result = await _userService.Authorize(loginDto);
        if (!result.IsSuccess)
        {
            return Unauthorized(result);
        }

        return Ok(result);
    }

    [HttpGet("info/{userId}")]
    public async Task<IActionResult> GetUserInfo(Guid userId)
    {
        var result = await _userService.GetUserInfo(userId);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}