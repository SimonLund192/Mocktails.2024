using Microsoft.AspNetCore.Mvc;
using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;

namespace Mocktails.WebApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartDAO _cartDAO;

    public ShoppingCartController(IShoppingCartDAO cartDAO)
    {
        _cartDAO = cartDAO;
    }

    [HttpGet("{sessionId}")]
    public async Task<ActionResult<IEnumerable<ShoppingCartItem>>> GetCartItems(string sessionId)
    {
        var items = await _cartDAO.GetCartItemsAsync(sessionId);
        return Ok(items);
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddToCart([FromBody] ShoppingCartItem item)
    {
        var itemId = await _cartDAO.AddToCartAsync(item);
        return Ok(itemId);
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

