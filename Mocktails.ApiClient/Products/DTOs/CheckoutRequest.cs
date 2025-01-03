namespace Mocktails.ApiClient.Products.DTOs;

public class CheckoutRequest
{
    public int UserId { get; set; }
    public string ShippingAddress { get; set; }
}
