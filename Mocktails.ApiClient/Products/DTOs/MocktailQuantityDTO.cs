namespace Mocktails.ApiClient.Products.DTOs;

public class MocktailQuantityDTO
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; }

    public decimal TotalPrice => Price * Quantity;
}
