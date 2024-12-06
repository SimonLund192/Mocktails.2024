using Mocktails.ApiClient.Orders.DTOs;

namespace Mocktails.WebApi.DTOs;

public class OrderDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending";
    public string ShippingAddress { get; set; }
    //public string PaymentMethod { get; set; } // Implement later
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<OrderItemDTO> OrderItems { get; set; } = new();

}
