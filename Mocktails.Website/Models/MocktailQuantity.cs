using Mocktails.ApiClient.Products.DTOs;

namespace Mocktails.Website.Models;

public class MocktailQuantity
{
    public int Id { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public string? Name { get; private set; }

    public MocktailQuantity(MocktailDTO mocktail, int quantity)
        : this(mocktail.Id, quantity, mocktail.Price, mocktail.Name)
    { }

    public MocktailQuantity(
        int id,
        int quantity,
        decimal price,
        string? name)
    {
        if (id <= 0)
            throw new ArgumentException("ID must be greater than zero.", nameof(id));

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        Id = id;
        Price = price;
        Name = name;
        Quantity = quantity;
    }

    public decimal GetTotalPrice()
    {
        return Price * Quantity;
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(newQuantity));

        Quantity = newQuantity;
    }
}
