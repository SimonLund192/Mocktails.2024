using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Orders.DTOs;
using Mocktails.ApiClient.Products;
using Mocktails.ApiClient.Products.DTOs;
using Mocktails.Website.Models;

namespace Mocktails.Website.Controllers;


public class CartControllerCookie : Controller
{
    private readonly IMocktailApiClient _mocktailApiClient;

    public CartControllerCookie(IMocktailApiClient mocktailApiClient)
    {
        _mocktailApiClient = mocktailApiClient;
    }

    public IActionResult Index()
    {
        var cart = GetCartFromCookie();
        return Ok(cart);
    }

    public async Task<IActionResult> Edit(int id, int quantity)
    {
        var mocktail = await _mocktailApiClient.GetMocktailByIdAsync(id);
        if (mocktail == null)
        {
            return NotFound($"Mocktail with ID {id} not found.");
        }

        var cart = LoadChangeAndSaveCart(cart => cart.ChangeQuantity(new MocktailQuantityDTO(id, quantity)));
        return Ok(cart);
    }

    public async Task<IActionResult> Add(int id, int quantity)
    {
        var mocktail = await _mocktailApiClient.GetMocktailByIdAsync(id);
        if (mocktail == null)
        {
            return NotFound($"Mocktail with ID {id} not found.");
        }

        var cart = LoadChangeAndSaveCart(cart => cart.ChangeQuantity(new MocktailQuantity(mocktail, quantity)));
        return Ok(cart);
    }

    public async Task<IActionResult> Delete(int id)
    {
        // Optionally, fetch mocktail details if needed
        var mocktail = await _mocktailApiClient.GetMocktailByIdAsync(id);
        if (mocktail == null)
        {
            return NotFound($"Mocktail with ID {id} not found.");
        }

        // Remove the mocktail from the cart asynchronously
        var cart = LoadChangeAndSaveCart(cart => cart.RemoveMocktail(id));
        return Ok(cart);
    }

    public async Task<IActionResult> EmptyCart()
    {
        // Clear the entire cart asynchronously if needed
        await Task.Run(() => LoadChangeAndSaveCart(cart => cart.EmptyAll()));
        return Ok("Cart cleared successfully.");
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

    public Cart GetCartFromCookie()
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
