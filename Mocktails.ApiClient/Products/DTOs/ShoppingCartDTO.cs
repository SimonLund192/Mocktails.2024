namespace Mocktails.ApiClient.Products.DTOs;

public class ShoppingCartDTO
{
    public int Id { get; set; }
    public string SessionId { get; set; }
    public int MocktailId { get; set; }
    public int Quantity { get; set; }
    public string MocktailName { get; set; }
    public decimal MocktailPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
