using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Products;
using Mocktails.ApiClient.Products.DTOs;
using Mocktails.Shared.Services;

namespace Mocktails.Website.Controllers;
public class ShoppingCartController : Controller
{
    private readonly ShoppingCartApiClient _cartApiClient;

    public ShoppingCartController(ShoppingCartApiClient cartApiClient)
    {
        _cartApiClient = cartApiClient;
    }

    public async Task<IActionResult> Index()
    {
        var cartItems = await _cartApiClient.GetCartItemsAsync();
        return View(cartItems);
    }

    public async Task<IActionResult> AddToCart(int mocktailId, int quantity = 1)
    {
        var cartItem = new ShoppingCartDTO
        {
            MocktailId = mocktailId,
            Quantity = quantity
        };

        await _cartApiClient.AddToCartAsync(cartItem);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> RemoveFromCart(int itemId)
    {
        await _cartApiClient.RemoveFromCartAsync(itemId);
        return RedirectToAction("Index");
    }
}
