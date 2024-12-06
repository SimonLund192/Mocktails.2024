using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Orders;
using Mocktails.ApiClient.Orders.DTOs;
using Mocktails.ApiClient.Products;
using Mocktails.ApiClient.Products.DTOs;
using Mocktails.Website.Models;

namespace Mocktails.Website.Controllers;

[Route("checkout")]
public class CheckoutController : Controller
{
    private readonly IMocktailApiClient _mocktailApiClient;
    private readonly IOrdersApiClient _ordersApiClient;

    public CheckoutController(IMocktailApiClient mocktailApiClient, IOrdersApiClient ordersApiClient)
    {
        _mocktailApiClient = mocktailApiClient;
        _ordersApiClient = ordersApiClient;
    }

    [HttpPost]
    public async Task<IActionResult> ProcessCheckout(int userId, string shippingAddress)
    {
        var cartCookieController = new CartControllerCookie(_mocktailApiClient);

        // Retrieve the cart from cookies
        var cart = cartCookieController.GetCartFromCookie();
        if (cart == null || !cart.MocktailQuantities.Any())
        {
            return BadRequest("Cart is empty.");
        }

        // Create the OrderDTO object
        var order = new OrderDTO
        {
            UserId = userId,
            OrderDate = DateTime.Now,
            TotalAmount = cart.GetTotal(),
            Status = "Pending",
            ShippingAddress = shippingAddress,
            OrderItems = cart.MocktailQuantities.Values.Select(mq => new OrderItemDTO
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
            // Create the order via the Orders API
            var orderId = await _ordersApiClient.CreateOrderAsync(order);

            // Update quantities in the Mocktail inventory
            foreach (var mocktailQuantity in cart.MocktailQuantities.Values)
            {
                var mocktail = await _mocktailApiClient.GetMocktailByIdAsync(mocktailQuantity.Id);
                var success = await _mocktailApiClient.UpdateMocktailQuantityAsync(
                    mocktailQuantity.Id,
                    -mocktailQuantity.Quantity // Reduce inventory
                );

                if (!success)
                {
                    return Conflict($"Insufficient stock for Mocktail ID: {mocktailQuantity.Id}.");
                }
            }

            // Clear the cart after successful checkout
            ClearCart();

            // Return a success response
            return Ok(new { OrderId = orderId, Message = "Order successfully created." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred during checkout: {ex.Message}");
        }
    }


    private void ClearCart()
    {
        // Clear the cart by deleting the cookie
        Response.Cookies.Delete("Cart");
    }
}
