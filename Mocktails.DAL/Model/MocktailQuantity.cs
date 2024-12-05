using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocktails.DAL.Model;
public class MocktailQuantity
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; }

    public MocktailQuantity(Mocktail mocktail, int quantity)
    {
        Id = mocktail.Id;
        Price = mocktail.Price;
        Name = mocktail.Name;
        Quantity = quantity;
    }

    public MocktailQuantity() { }

    public int GetTotalPrice()
    {
        return Price * Quantity;
    }
}
