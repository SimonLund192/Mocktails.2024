using System.ComponentModel.DataAnnotations;

namespace Mocktails.WebApi.DTOs;

public class CreateOrderRequest
{
    [Required]
    public string ShippingAddress { get; set; } = "";

    [Required]
    public int UserId { get; set; }

    public List<Product> Products { get; set; } = [];

    public class Product
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}
