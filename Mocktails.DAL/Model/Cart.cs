using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocktails.DAL.Model;
public class Cart
{

    public Dictionary<int, MocktailQuantity> MocktailQuantities { get; set; }
    public Cart(Dictionary<int, MocktailQuantity>? mocktailQuantities = null)
    {
        MocktailQuantities = mocktailQuantities ?? new Dictionary<int, MocktailQuantity>();
    }

    public void ChangeQuantity(MocktailQuantity mocktailQuantity)
    {
        if (MocktailQuantities.ContainsKey(mocktailQuantity.Id))
        {
            MocktailQuantities[mocktailQuantity.Id].Quantity += mocktailQuantity.Quantity;
            if (MocktailQuantities[mocktailQuantity.Id].Quantity <= 0)
            {
                MocktailQuantities.Remove(mocktailQuantity.Id);
            }
        }
        else
        {
            MocktailQuantities[mocktailQuantity.Id] = mocktailQuantity;
        }
    }

    public void RemoveMocktail(int mocktailId) => MocktailQuantities.Remove(mocktailId);
    public void Update(int mocktailId, int quantity) => MocktailQuantities[mocktailId].Quantity = quantity;

    public decimal GetTotal()
    {
        decimal total = 0;
        foreach(MocktailQuantity mocktailQuantity in MocktailQuantities.Values)
        {
            total += mocktailQuantity.GetTotalPrice();
        }
        return total;
    }

    public int GetNumberOfProducts() => MocktailQuantities.Sum(mq => mq.Value.Quantity);
    public void EmptyAll() => MocktailQuantities.Clear();
}
