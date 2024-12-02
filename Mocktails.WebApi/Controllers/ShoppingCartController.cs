using Microsoft.AspNetCore.Mvc;
using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;
using Mocktails.WebApi.DTOs;
using Mocktails.Shared.Services;

namespace Mocktails.WebApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartDAO _cartDAO;
    private readonly ShoppingCartService _cartService;

    public ShoppingCartController(IShoppingCartDAO cartDAO, ShoppingCartService cartService)
    {
        _cartDAO = cartDAO;
        _cartService = cartService;
    }

    [HttpGet("{sessionId}")]
    public async Task<ActionResult<IEnumerable<ShoppingCartItem>>> GetCartItems(string sessionId)
    {
        var items = await _cartDAO.GetCartItemsAsync(sessionId);
        return Ok(items);
    }

    //[HttpPost]
    //public async Task<ActionResult<int>> AddToCart([FromBody] ShoppingCartItem item)
    //{
    //    var itemId = await _cartDAO.AddToCartAsync(item);
    //    return Ok(itemId);
    //}

    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] ShoppingCartDTO cartItem)
    {
        if (cartItem == null || cartItem.MocktailId <= 0 || cartItem.Quantity <= 0)
        {
            return BadRequest("Invalid cart item. Ensure MocktailId and Quantity are valid.");
        }

        var sessionId = _cartService.GetOrCreateSessionId();
        cartItem.SessionId = sessionId;

        try
        {
            // Map the DTO to the database entity
            var dbItem = new ShoppingCartItem
            {
                SessionId = cartItem.SessionId,
                MocktailId = cartItem.MocktailId,
                MocktailName = cartItem.MocktailName,
                Quantity = cartItem.Quantity,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _cartDAO.AddToCartAsync(dbItem);
            return Ok("Item added to cart successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error adding item to cart: {ex.Message}");
        }
    }



    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCartItem(int id, [FromBody] ShoppingCartItem item)
    {
        item.Id = id;
        var updated = await _cartDAO.UpdateCartItemAsync(item);
        if (!updated) return NotFound();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveFromCart(int id)
    {
        var removed = await _cartDAO.RemoveFromCartAsync(id);
        if (!removed) return NotFound();
        return Ok();
    }
}

