using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Products;
using Mocktails.Website.Services;

namespace Mocktails.Website.Controllers;

[Route("cart")]
public class CartController : Controller
{
    private readonly IMocktailApiClient _mocktailApiClient;
    private readonly ICartService _cartService;

    public CartController(
        IMocktailApiClient mocktailApiClient,
        ICartService cartService)
    {
        _mocktailApiClient = mocktailApiClient;
        _cartService = cartService;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        var cart = _cartService.GetCart();
        return View(cart);
    }

    [HttpGet("Add/{id}")]
    public async Task<IActionResult> Add(int id, int quantity = 1)
    {
        var mocktail = await _mocktailApiClient.GetMocktailByIdAsync(id);
        if (mocktail is null)
            return NotFound($"Mocktail with ID {id} not found.");

        var cart = _cartService.LoadChangeAndSaveCart(cart => cart.AddOrAdjustProduct(new(mocktail, quantity)));

        return RedirectToAction(nameof(Index));
        //return NoContent();
    }

    [HttpGet("Delete/{id}")]
    public IActionResult Delete(int id)
    {
        var cart = _cartService.LoadChangeAndSaveCart(cart => cart.RemoveProduct(id));

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Checkout")]
    public IActionResult Checkout()
    {
        var cart = _cartService.GetCart();

        if (cart == null || cart.IsEmpty)
        {
            TempData["ErrorMessage"] = "Your cart is empty. Add some mocktails to proceed!";
            return RedirectToAction(nameof(Index));
        }

        // Provide the cart to the checkout view
        return View(cart);
    }

    [HttpPost("EmptyCart")]
    public IActionResult EmptyCart()
    {
        _cartService.LoadChangeAndSaveCart(cart => cart.EmptyAll());

        return RedirectToAction(nameof(Index));
    }

    #region Move to Checkoutcontroller
    //[HttpGet("Checkout")]
    //public async Task<IActionResult> Checkout()
    //{
    //    var cart = GetCartFromCookie();

    //    if (cart == null || !cart.MocktailQuantities.Any())
    //    {
    //        TempData["ErrorMessage"] = "Your cart is empty. Add some mocktails to proceed!";
    //        return RedirectToAction("Index");
    //    }

    //    // Example checkout page or summary (optional)
    //    return View("Checkout", cart);
    //}

    //[HttpPost("Checkout")]
    //public async Task<IActionResult> CompleteCheckout(string shippingAddress)
    //{
    //    var cart = GetCartFromCookie();

    //    if (cart == null || !cart.MocktailQuantities.Any())
    //    {
    //        TempData["ErrorMessage"] = "Your cart is empty.";
    //        return RedirectToAction("Index");
    //    }

    //    // Logic to save order and order items
    //    try
    //    {
    //        // Call your API client to create an order
    //        var orderDto = new OrderDTO
    //        {
    //            UserId = 1, // Replace with logged-in user ID
    //            OrderDate = DateTime.Now,
    //            TotalAmount = cart.GetTotal(),
    //            Status = "Pending",
    //            ShippingAddress = shippingAddress,
    //            OrderItems = cart.MocktailQuantities.Values.Select(mq => new OrderItemDTO
    //            {
    //                MocktailId = mq.Id,
    //                Quantity = mq.Quantity,
    //                Price = mq.Price,
    //                CreatedAt = DateTime.Now,
    //                UpdatedAt = DateTime.Now
    //            }).ToList()
    //        };

    //        var orderId = await _ordersApiClient.CreateOrderAsync(orderDto);

    //        // Clear the cart after successful checkout
    //        EmptyCart();

    //        TempData["SuccessMessage"] = "Order placed successfully!";
    //        return RedirectToAction("OrderConfirmation", new { orderId });
    //    }
    //    catch (Exception ex)
    //    {
    //        TempData["ErrorMessage"] = $"Error during checkout: {ex.Message}";
    //        return RedirectToAction("Index");
    //    }
    //}
    #endregion
}
