using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestoRise.BuisnessLogic.DTOs;
using RestoRise.BuisnessLogic.Interfaces;

namespace RestoRise.Api.Controllers.Restaurants;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("restaurnat")]
public class RestaurantController:ControllerBase
{
    private readonly IRestaurnatService _restaurnatService;

    public RestaurantController(IRestaurnatService restaurnatService)
    {
        _restaurnatService = restaurnatService;
    }

    [HttpGet("all/cityId")]
    public async Task<IActionResult> GetAll(int cityId)
    {
       var result = await _restaurnatService.GetAllRestaurants();
       if (result.IsSuccess)
       {
           return Ok(result);
       }
       else
       {
           return BadRequest(result);
       }
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateRestaurantDto createRestaurantDto)
    {
        var result =  await _restaurnatService.CreateRestaurant(createRestaurantDto);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }
}