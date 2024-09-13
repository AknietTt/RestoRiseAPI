using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestoRise.Application.DTOs;
using RestoRise.Application.DTOs.Staff;
using RestoRise.Application.Interfaces.Services;

namespace RestoRise.Api.Controllers;

[ApiController]
[Route("employee")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [Authorize(Roles = "Owner, Admin")]
    [HttpPost("add")]
    public async Task<IActionResult> AddEmployee([FromBody] StaffRegisterDto dto)
    {
        var result = await _employeeService.AddEmployee(dto);
        if (result.IsSuccess) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Roles = "Owner, Admin")]
    [HttpGet("owner")]
    public async Task<IActionResult> GetStaves([FromQuery] Guid ownerId)
    {
        var result = await _employeeService.GetStavesByOwner(ownerId);
        if (result.IsSuccess) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetStavesInfoById([FromQuery] Guid staffId)
    {
        var result = await _employeeService.GetStaffById(staffId);
        if (result.IsSuccess) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("roles")]
    [AllowAnonymous]
    public async Task<IActionResult> GetRoles()
    {
        var result = await _employeeService.GetRoles();
        if (result.IsSuccess) return Ok(result);
        return BadRequest(result);
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateStaff([FromBody] StaffUpdateDto dto)
    {
        var result = await _employeeService.UpdateStaff(dto);
        if (result.IsSuccess) return Ok(result);
        return BadRequest(result);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _employeeService.Authorize(loginDto);
        if (!result.IsSuccess)
        {
            return Unauthorized(result);
        }

        return Ok(result);
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteStaff([FromQuery] Guid staffId)
    {
        var result = await _employeeService.DeleteStaff(staffId);
        if (result.IsSuccess) return Ok(result);
        return BadRequest(result);
    }
}