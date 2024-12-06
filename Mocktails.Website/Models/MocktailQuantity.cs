using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.ApiClient.Products.DTOs;

namespace Mocktails.Website.Models;
public class MocktailQuantity
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; }

    public MocktailQuantity(MocktailDTO mocktail, int quantity)
    {
        Id = mocktail.Id;
        Price = mocktail.Price;
        Name = mocktail.Name;
        Quantity = quantity;
    }

    public MocktailQuantity() { }

    public decimal GetTotalPrice()
    {
        return Price * Quantity;
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity < 0)
        {
            throw new ArgumentException("Quantity cannot be negative.");
        }
        Quantity = newQuantity;
    }
}
