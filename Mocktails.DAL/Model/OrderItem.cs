namespace Mocktails.DAL.Model;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int MocktailId { get; set; }
    public string? MocktailName { get; set; }
    public string? MocktailDescription { get; set; }
    public string? MocktailImageUrl { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
