using Mocktails.Website.Models;

namespace Mocktails.Website.Services;

public interface ICartService
{
    /// <summary>
    /// Saves the cart.
    /// </summary>
    /// <param name="cart">The <see cref="Cart"/>.</param>
    void SaveCart(Cart cart);

    /// <summary>
    /// Gets the current cart.
    /// </summary>
    /// <returns>A <see cref="Cart"/>.</returns>
    Cart GetCart();

    Cart LoadChangeAndSaveCart(Action<Cart> action);
}
