using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestoRise.Application.DTOs.Order;
using RestoRise.Application.Interfaces.Services;

namespace RestoRise.Api.Controllers;
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("Order") ]
public class OrderController:ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [AllowAnonymous]
    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder([FromBody] AddOrderDto dto )
    {
     
        var result = await _orderService.CreateOrder(dto);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("owner/{ownerId}")]
    public async Task<IActionResult> GetOredersByOwner(Guid ownerId)
    {
        var result = await _orderService.GetOredersByOwner(ownerId);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}

