using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Orders;
using Mocktails.ApiClient.Orders.DTOs;
using Mocktails.Website.Services;

namespace Mocktails.Website.Controllers;

[Route("checkout")]
public class CheckoutController : Controller
{
    private readonly IOrdersApiClient _ordersApiClient;
    private readonly ICartService _cartService;

    public CheckoutController(
        IOrdersApiClient ordersApiClient,
        ICartService cartService)
    {
        _ordersApiClient = ordersApiClient;
        _cartService = cartService;
    }

    [HttpPost("place-order")]
    public async Task<IActionResult> PlaceOrder([FromForm] string shippingAddress)
    {
        var cart = _cartService.GetCart();

        if (cart is null || cart.IsEmpty)
        {
            TempData["ErrorMessage"] = "Your cart is ??";
            return RedirectToAction("Index");
        }

        #region UserIdClaim
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
        {
            TempData["ErrorMessage"] = "You must be logged in";
            return RedirectToAction("Index");
        }

        var userId = int.Parse(userIdClaim.Value);
        #endregion

        var orderRequest = new CreateOrderRequest
        {
            UserId = userId,
            ShippingAddress = shippingAddress,
            Products = cart.Products.Select(p => new CreateOrderRequest.Product
            {
                Id = p.Id,
                Quantity = p.Quantity,
            }).ToList()
        };

        try
        {
            // Pass the OrderDTO to CreateOrderAsync
            var orderId = await _ordersApiClient.CreateOrderAsync(orderRequest);

            _cartService.LoadChangeAndSaveCart(cart => cart.EmptyAll());


            return RedirectToAction("Receipt", new { orderId });
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"An error occurred while placing the order: {ex.Message}";
            return RedirectToAction("Index", "Cart");
        }
    }

    [HttpGet("order-receipt/{orderId}")]
    public IActionResult Receipt([FromRoute] int orderId)
    {
        return View(orderId);
    }
}
