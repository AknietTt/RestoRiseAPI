using Microsoft.AspNetCore.Mvc;
using RestoRise.BuisnessLogic.DTOs.Branch;
using RestoRise.BuisnessLogic.Interfaces;

namespace RestoRise.Api.Controllers;
[ApiController]
[Route("[controller]")]
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
}