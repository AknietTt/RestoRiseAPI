using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestoRise.Application.DTOs.Restaurant;
using RestoRise.Application.Interfaces.Services;

namespace RestoRise.Api.Controllers.Restaurants;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("restaurant")]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurnatService _restaurnatService;

    public RestaurantController(IRestaurnatService restaurnatService)
    {
        _restaurnatService = restaurnatService;
    }

    #region GET

    [AllowAnonymous]
    [HttpGet("all/{cityId}")]
    public async Task<IActionResult> GetAll(Guid cityId)
    {
        var result = await _restaurnatService.GetAllRestaurants(cityId);
        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    [AllowAnonymous]
    [HttpGet("search/{text}")]
    public async Task<IActionResult> Search(string text)
    {
        var result = await _restaurnatService.SearchRestaurants(text);
        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("{ownerId}")]
    public async Task<IActionResult> GetByOwner(Guid ownerId)
    {
        var result = await _restaurnatService.GetRestaurantsByOwner(ownerId);
        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _restaurnatService.GetAllRestaurants();
        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _restaurnatService.GetRestaurantById(id);

        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    #endregion

    #region POST

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] RestaurantCreateDto restaurantCreateDto)
    {
        var result = await _restaurnatService.CreateRestaurant(restaurantCreateDto);
        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    #endregion

    #region PUT

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update([FromBody] RestaurantUpdateDto restaurnatUpdateDto, Guid id)
    {
        if (restaurnatUpdateDto.Id != id)
            return BadRequest("Не равно id переданной в параметрах и в теле запроса restaurant.id != id");
        var result = await _restaurnatService.UpdateRestaurant(restaurnatUpdateDto);
        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    #endregion

    #region DELETE

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _restaurnatService.Delete(id);
        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    #endregion
}