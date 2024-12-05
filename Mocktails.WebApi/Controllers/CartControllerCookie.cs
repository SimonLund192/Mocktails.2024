using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;

namespace Mocktails.WebApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CartControllerCookie : Controller
{
    private readonly IMocktailDAO _mocktailDAO;
    private readonly IOrderDAO _orderDAO;

    public CartControllerCookie(IMocktailDAO mocktailDAO, IOrderDAO orderDAO)
    {
        _mocktailDAO = mocktailDAO;
        _orderDAO = orderDAO;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var cart = GetCartFromCookie();
        return Ok(cart);
    }

    [HttpPut("{id}/{quantity}")]
    public async Task<IActionResult> Edit(int id, int quantity)
    {
        var mocktail = await _mocktailDAO.GetMocktailByIdAsync(id);
        if (mocktail == null)
        {
            return NotFound($"Mocktail with ID {id} not found.");
        }

        var cart = LoadChangeAndSaveCart(cart => cart.ChangeQuantity(new MocktailQuantity(mocktail, quantity)));
        return Ok(cart);
    }

    [HttpPost("{id}/{quantity}")]
    public async Task<IActionResult> Add(int id, int quantity)
    {
        var mocktail = await _mocktailDAO.GetMocktailByIdAsync(id);
        if (mocktail == null)
        {
            return NotFound($"Mocktail with ID {id} not found.");
        }

        var cart = LoadChangeAndSaveCart(cart => cart.ChangeQuantity(new MocktailQuantity(mocktail, quantity)));
        return Ok(cart);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // Optionally, fetch mocktail details if needed
        var mocktail = await _mocktailDAO.GetMocktailByIdAsync(id);
        if (mocktail == null)
        {
            return NotFound($"Mocktail with ID {id} not found.");
        }

        // Remove the mocktail from the cart asynchronously
        var cart = LoadChangeAndSaveCart(cart => cart.RemoveMocktail(id));
        return Ok(cart);
    }

    [HttpDelete("clear")]
    public async Task<IActionResult> EmptyCart()
    {
        // Clear the entire cart asynchronously if needed
        await Task.Run(() => LoadChangeAndSaveCart(cart => cart.EmptyAll()));
        return Ok("Cart cleared successfully.");
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout(int userId, string shippingAddress)
    {
        // Retrieve the cart from the cookie
        var cart = GetCartFromCookie();

        if (cart == null || !cart.MocktailQuantities.Any())
        {
            return BadRequest("Cart is empty.");
        }

        // Create the Order object
        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.Now,
            TotalAmount = cart.GetTotal(),
            Status = "Pending",
            ShippingAddress = shippingAddress,
            OrderItems = cart.MocktailQuantities.Values.Select(mq => new OrderItem
            {
                MocktailId = mq.Id,
                Quantity = mq.Quantity,
                Price = mq.Price,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }).ToList()
        };

        try
        {
            // Save the order and its items using OrderDAO
            var orderId = await _orderDAO.CreateOrderAsync(order);

            // Update the quantity of each mocktail in the database
            foreach (var mocktailQuantity in cart.MocktailQuantities.Values)
            {
                // Fetch the current RowVersion from the database
                var mocktail = await _mocktailDAO.GetMocktailByIdAsync(mocktailQuantity.Id);
                var rowVersion = mocktail.RowVersion;

                // Attempt to update the quantity
                var success = await _mocktailDAO.UpdateMocktailQuantityAsync(
                mocktailQuantity.Id,
                -mocktailQuantity.Quantity, // Negative value for subtraction
                rowVersion
                );

                if (!success)
                {
                    return Conflict($"Insufficient stock for Mocktail ID: {mocktailQuantity.Id}. Please adjust your cart.");
                }
            }

            // Clear the cart
            await EmptyCart();

            // Return success response
            return Ok(new { OrderId = orderId, Message = "Order created successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred during checkout: {ex.Message}");
        }
    }


    private void SaveCartToCookie(Cart cart)
    {
        // Serialize the cart and save it to a cookie
        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(7),
            Path = "/"
        };

        Response.Cookies.Append("Cart", JsonSerializer.Serialize(cart), cookieOptions);
    }

    private Cart GetCartFromCookie()
    {
        // Retrieve the cart from the cookie
        Request.Cookies.TryGetValue("Cart", out string? cookie);
        if (cookie == null)
        {
            return new Cart();
        }

        return JsonSerializer.Deserialize<Cart>(cookie) ?? new Cart();
    }

    private Cart LoadChangeAndSaveCart(Action<Cart> action)
    {
        // Load the cart, apply changes, and save it back to the cookie
        var cart = GetCartFromCookie();
        action(cart);
        SaveCartToCookie(cart);

        return cart;
    }
}
