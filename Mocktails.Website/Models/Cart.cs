namespace Mocktails.Website.Models;

public class Cart
{
    /// <summary>
    /// The cart state.
    /// </summary>
    /// <remarks>
    /// Made private to protect/hide cart state.
    /// State can only be manipulated through the defined cart methods.
    /// </remarks>
    private readonly Dictionary<int, MocktailQuantity> _products;

    public Cart(Dictionary<int, MocktailQuantity>? products = null)
    {
        _products = products ?? [];
    }

    /// <summary>
    /// Tries to adjust the product quantity.
    /// If not found, the product is added to the cart.
    /// </summary>
    /// <param name="product">The product with quantity.</param>
    public void AddOrAdjustProduct(MocktailQuantity product)
    {
        if (_products.TryGetValue(product.Id, out MocktailQuantity? p))
        {
            var newQuantity = p.Quantity + product.Quantity;

            if (newQuantity > 0)
            {
                _products[product.Id].UpdateQuantity(newQuantity);
            }
            else
            {
                _products.Remove(product.Id);
            }
        }
        else
        {
            _products[product.Id] = product;
        }
    }

    /// <summary>
    /// Removes a product from the cart, if it exists.
    /// </summary>
    /// <param name="productId">The product ID.</param>
    public void RemoveProduct(int productId) => _products.Remove(productId);

    /// <summary>
    /// Calculates the carts total price.
    /// </summary>
    /// <returns>The total price as <see cref="decimal"/>.</returns>
    public decimal GetTotal()
    {
        decimal total = 0;
        foreach (MocktailQuantity mocktailQuantity in _products.Values)
        {
            total += mocktailQuantity.GetTotalPrice();
        }
        return total;
    }

    /// <summary>
    /// Gets the number of items in the cart.
    /// </summary>
    /// <returns>The number of items as <see cref="int"/>.</returns>
    public int GetNumberOfProducts() => _products.Sum(mq => mq.Value.Quantity);

    /// <summary>
    /// Empties the cart.
    /// </summary>
    public void EmptyAll() => _products.Clear();

    /// <summary>
    /// <see langword="true"/> if the cart is empty; Otherwise, <see langword="false"/>.
    /// </summary>
    public bool IsEmpty => _products.Count == 0;

    public List<MocktailQuantity> Products => _products.Select(mq => mq.Value).ToList();
}
