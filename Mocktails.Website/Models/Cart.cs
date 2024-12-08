namespace Mocktails.Website.Models;

public class Cart
{
    private readonly Dictionary<int, MocktailQuantity> _mocktailQuantities;

    public Cart(Dictionary<int, MocktailQuantity>? mocktailQuantities = null)
    {
        _mocktailQuantities = mocktailQuantities ?? [];
    }

    public void ChangeQuantity(MocktailQuantity mocktailQuantity)
    {
        if (_mocktailQuantities.TryGetValue(mocktailQuantity.Id, out MocktailQuantity? product))
        {
            product.Quantity += mocktailQuantity.Quantity;
            if (_mocktailQuantities[mocktailQuantity.Id].Quantity <= 0)
            {
                _mocktailQuantities.Remove(mocktailQuantity.Id);
            }
        }
        else
        {
            _mocktailQuantities[mocktailQuantity.Id] = mocktailQuantity;
        }
    }

    public void RemoveMocktail(int mocktailId) => _mocktailQuantities.Remove(mocktailId);

    public void Update(int mocktailId, int quantity) => _mocktailQuantities[mocktailId].Quantity = quantity;

    public decimal GetTotal()
    {
        decimal total = 0;
        foreach (MocktailQuantity mocktailQuantity in _mocktailQuantities.Values)
        {
            total += mocktailQuantity.GetTotalPrice();
        }
        return total;
    }

    public int GetNumberOfProducts() => _mocktailQuantities.Sum(mq => mq.Value.Quantity);

    public void EmptyAll() => _mocktailQuantities.Clear();

    public bool IsEmpty => _mocktailQuantities.Count == 0;

    public List<MocktailQuantity> Products => _mocktailQuantities.Select(mq => mq.Value).ToList();
}
