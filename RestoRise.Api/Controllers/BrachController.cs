using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestoRise.Application.DTOs.Branch;
using RestoRise.Application.Interfaces.Services;

namespace RestoRise.Api.Controllers;
[ApiController]
[Route("branch")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class BrachController:ControllerBase
{
    private readonly IBranchService _branchService;

    public BrachController(IBranchService branchService)
    {
        _branchService = branchService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result =  await _branchService.GetAllBranch();
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> Create([FromBody] BranchCreateDto dto)
    {
        var result =  await _branchService.CreateBranch(dto);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update([FromBody] BranchUpdateDto dto , Guid id)
    {
        var result = await _branchService.UpdateBranch(dto);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _branchService.DeleteBranch(id);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }
  
    [HttpGet("restaurant/{restaurantId}")]
    public async Task<IActionResult> GetByRestaurantId(Guid restaurantId)
    {
        var result = await _branchService.GetByRestaurant(restaurantId);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }
    
    [AllowAnonymous]
    [HttpGet("restaurant/{restaurantId}/{cityId}")]
    public async Task<IActionResult> GetByRestaurantId(Guid restaurantId , Guid cityId)
    {
        var result = await _branchService.GetByRestaurant(restaurantId , cityId);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _branchService.GetBranchById(id);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }
    
    [HttpGet("owner/{ownerId}")]
    public async Task<IActionResult> GetByOwner(Guid ownerId)
    {
        var result = await _branchService.GetByOwner(ownerId);
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