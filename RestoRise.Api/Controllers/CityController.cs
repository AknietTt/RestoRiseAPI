﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestoRise.Application.DTOs.City;
using RestoRise.Application.Interfaces.Services;


namespace RestoRise.Api.Controllers;

[ApiController]
[Route("city") ]
[Authorize(AuthenticationSchemes = "Bearer")]
public class CityController:ControllerBase
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [AllowAnonymous]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllCity()
    {
        var res =  await _cityService.GetAllCity();
        if (!res.IsSuccess)
        {
            return BadRequest(res);
        }
        return Ok(res);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCity([FromQuery] string name)
    {
        var res = await _cityService.CreateCity(name);
        if (!res.IsSuccess)
        {
            return BadRequest(res);
        }
        return Ok(res);
    }

    
    [HttpPut("update/id")]
    public async Task<IActionResult> UpdateCity(Guid id , [FromBody] CityDto cityUpdateDto)
    {
        
        if (id == cityUpdateDto.Id)
        {
            var res = await _cityService.UpdateCity(cityUpdateDto);
            if (!res.IsSuccess)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        return BadRequest("Не совпадает id DTO и id из параметра");

    }
    
   
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteCity(Guid id)
    {
        var res = await _cityService.DeleteCity(id);
        if (!res.IsSuccess)
        {
            return BadRequest(res);
        }
        return Ok(res);
    }

    
}