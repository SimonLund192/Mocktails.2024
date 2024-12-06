using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Products;
using Mocktails.ApiClient.Products.DTOs;
using Mocktails.Website.Models;

namespace Mocktails.Website.Controllers;


[Route("ShoppingCart")] // Route prefix for the controller
[ApiController]
public class CartCookieController : Controller
{
    private readonly IMocktailApiClient _mocktailApiClient;

    public CartCookieController(IMocktailApiClient mocktailApiClient)
    {
        _mocktailApiClient = mocktailApiClient;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var cart = GetCartFromCookie();
        return View("Index", cart); // Ensure "Cart" matches the view file name
    }

    public async Task<IActionResult> Edit(int id, int quantity)
    {
        var mocktail = await _mocktailApiClient.GetMocktailByIdAsync(id);
        if (mocktail == null)
        {
            return NotFound($"Mocktail with ID {id} not found.");
        }

        var cart = LoadChangeAndSaveCart(cart =>
        {
            if (cart.MocktailQuantities.ContainsKey(id))
            {
                cart.MocktailQuantities[id].Quantity = quantity;
            }
        });

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Add(int id, int quantity)
    {

        var mocktail = await _mocktailApiClient.GetMocktailByIdAsync(id);
        if (mocktail == null)
        {
            return NotFound($"Mocktail with ID {id} not found.");
        }

        var cart = LoadChangeAndSaveCart(cart =>
        {
            if (cart.MocktailQuantities.ContainsKey(id))
            {
                cart.MocktailQuantities[id].Quantity += quantity;
            }
            else
            {
                cart.MocktailQuantities[id] = new MocktailQuantity
                {
                    Id = mocktail.Id,
                    Name = mocktail.Name,
                    Price = mocktail.Price,
                    Quantity = mocktail.Quantity
                };
            }
        });

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var cart = LoadChangeAndSaveCart(cart => cart.RemoveMocktail(id));
        return RedirectToAction("Index");
    }

    public IActionResult EmptyCart()
    {
        LoadChangeAndSaveCart(cart => cart.EmptyAll());
        return RedirectToAction("Index");
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
