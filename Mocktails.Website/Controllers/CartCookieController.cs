using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Orders;
using Mocktails.ApiClient.Orders.DTOs;
using Mocktails.ApiClient.Products;
using Mocktails.ApiClient.Products.DTOs;
using Mocktails.Website.Models;

namespace Mocktails.Website.Controllers;


[Route("ShoppingCart")]
[ApiController]
public class CartCookieController : Controller
{
    private readonly IMocktailApiClient _mocktailApiClient;
    //private readonly IOrdersApiClient _ordersApiClient;

    public CartCookieController(IMocktailApiClient mocktailApiClient/*, IOrdersApiClient ordersApiClient*/)
    {
        _mocktailApiClient = mocktailApiClient;
        //_ordersApiClient = ordersApiClient;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var cart = GetCartFromCookie();
        return View("Index", cart); // Ensure "Cart" matches the view file name
    }

    [HttpPut("Edit/{id}")]
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

        return View("Index", cart);
    }

    // TODO: Figure out HOW TO FIX THIS

    //public async Task<ActionResult> Add(int id, int quantity)
    //{
    //    // Fetch the mocktail from the API
    //    var mocktail = await _mocktailApiClient.GetMocktailByIdAsync(id);
    //    if (mocktail == null)
    //    {
    //        return NotFound($"Mocktail with ID {id} not found.");
    //    }

    //    // Load the cart, add the item, and save it back
    //    var cart = LoadChangeAndSaveCart(cart =>
    //    {
    //        cart.ChangeQuantity(new MocktailQuantityDTO
    //        {
    //            Id = mocktail.Id,
    //            Name = mocktail.Name,
    //            Price = mocktail.Price,
    //            Quantity = quantity
    //        });
    //    });

    //    return RedirectToAction("Index");
    //}

    [HttpGet("Add/{id}")]
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
                    Quantity = quantity
                };
            }
        });

        return RedirectToAction("Index");
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult Delete(int id)
    {
        var cart = LoadChangeAndSaveCart(cart => cart.RemoveMocktail(id));
        return RedirectToAction("Index");
    }

    [HttpGet("Checkout")]
    public IActionResult Checkout()
    {
        var cart = GetCartFromCookie();

        if (cart == null || !cart.MocktailQuantities.Any())
        {
            TempData["ErrorMessage"] = "Your cart is empty. Add some mocktails to proceed!";
            return RedirectToAction("Index");
        }

        // Provide the cart to the checkout view
        return View("Checkout", cart); // Ensure there's a "Checkout" view.
    }

    public IActionResult EmptyCart()
    {
        var cart = LoadChangeAndSaveCart(cart => cart.EmptyAll());
        return RedirectToAction("Index", cart);
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
