using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RestoRise.BuisnessLogic.DTOs;
using RestoRise.BuisnessLogic.Interfaces;

namespace RestoRise.Api.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class AuthController:ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("registration/user")]
    public async Task<IActionResult> Registration([FromBody] UserCreateDto registration)
    {
        var result = await _userService.Register(registration);
        return Ok(result);
    }

}