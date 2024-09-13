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

    [HttpGet("{ownerId}/{statusCode}")]
    public async Task<IActionResult> GetOredersByUserId(Guid ownerId, int statusCode)
    {
        var result = await _orderService.GetOrdersByUserId(ownerId, statusCode);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("detail/{orderId}")]
    public async Task<IActionResult> GetOrderDetail(Guid orderId)
    {
        var result = await _orderService.GetOrderDetail(orderId);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPut("status/{orderId}/{statusCode}")]
    public async Task<IActionResult> EditStatusOrder(Guid orderId, int statusCode)
    {
        var result = await _orderService.EditStatusOrder(orderId, statusCode);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}

