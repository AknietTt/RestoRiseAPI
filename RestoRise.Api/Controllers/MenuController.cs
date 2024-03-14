using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestoRise.Application.Interfaces.Services;

namespace RestoRise.Api.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("menu")]
public class MenuController:ControllerBase
{
    private readonly IFoodService _foodService;

    public MenuController(IFoodService foodService)
    {
        _foodService = foodService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddFood()
    {
        return Ok();
    }
}