namespace Mocktails.Website.Models;

public class CartViewModel
{
    public Dictionary<int, MocktailQuantity> Items { get; set; }
    public decimal TotalPrice { get; set; }
    public int TotalItems { get; set; }
}
