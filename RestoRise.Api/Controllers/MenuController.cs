using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestoRise.Application.DTOs.Foods;
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

    [HttpPost("food/add")]
    public async Task<IActionResult> AddFood([FromBody] FoodCreateDto dto)
    {
        var result = await _foodService.CraeteFood(dto);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("food")]
    public async Task<IActionResult> GetFoodByRestaurant([FromQuery] Guid restaurnatId)
    {
        var result = await _foodService.GetFoodsByRestaurant(restaurnatId);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPut("food/update/{id}")]
    public async Task<IActionResult> UpdateFood([FromBody] FoodUpdateDto dto , Guid id)
    {
        if (dto.Id != id)
        {
            return BadRequest("Не совапдает id");
        }
        var result = await _foodService.UpdateFood(dto);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);  
    }
    
    [HttpDelete("food/delete/{id}")]
    public async Task<IActionResult> DeleteFood(Guid id)
    {
        var result = await _foodService.DeleteFood(id);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}