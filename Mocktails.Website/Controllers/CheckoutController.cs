using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Orders;
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

        var isSuccess = true;
        var orderId = 1337;

        return isSuccess
            ? RedirectToAction("Receipt", new { orderId = orderId })
            : RedirectToAction("Index", "Cart");
    }

    [HttpGet("order-receipt/{orderId}")]
    public IActionResult Receipt([FromRoute] int orderId)
    {
        return View(orderId);
    }
}
