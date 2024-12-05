namespace Mocktails.WebApi.DTOs;

public class CheckoutRequest
{
    public int UserId { get; set; }
    public string ShippingAddress { get; set; }
}
