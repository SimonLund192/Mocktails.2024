using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Products;
using Mocktails.ApiClient.Products.DTOs;
using Mocktails.Shared.Services;

namespace Mocktails.Website.Controllers;
public class ShoppingCartController : Controller
{
    private readonly ShoppingCartApiClient _cartApiClient;
    private readonly ShoppingCartService _cartService;

    public ShoppingCartController(ShoppingCartApiClient cartApiClient, ShoppingCartService cartService)
    {
        _cartApiClient = cartApiClient;
        _cartService = cartService;
    }

    public async Task<IActionResult> Index()
    {
        var sessionId = _cartService.GetOrCreateSessionId();
        var cartItems = await _cartApiClient.GetCartItemsAsync(sessionId);
        return View(cartItems);
    }



    #region AddToCart C# and Javascript

    public async Task<IActionResult> AddToCart(int mocktailId, int quantity = 1)
    {
        var sessionId = _cartService.GetOrCreateSessionId();
        var cartItem = new ShoppingCartDTO
        {
            SessionId = sessionId,
            MocktailId = mocktailId,
            Quantity = quantity
        };

        await _cartApiClient.AddToCartAsync(cartItem);
        return RedirectToAction("Index");
    }
    #endregion
    public async Task<IActionResult> RemoveFromCart(int itemId)
    {
        await _cartApiClient.RemoveFromCartAsync(itemId);
        return RedirectToAction("Index");
    }
}
