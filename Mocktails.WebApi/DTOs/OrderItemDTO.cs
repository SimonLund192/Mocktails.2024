namespace Mocktails.WebApi.DTOs;

public class OrderItemDAO
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int MocktailId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
