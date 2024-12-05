using System.Collections.Generic;
using System.Threading.Tasks;
using Mocktails.ApiClient.Products.DTOs;

namespace Mocktails.ApiClient.Products
{
    public interface IShoppingCartApiClient
    {
        /// <summary>
        /// Retrieves all items in the shopping cart with detailed information.
        /// </summary>
        /// <returns>A list of shopping cart items with details.</returns>
        Task<IEnumerable<ShoppingCartDTO>> GetCartItemsAsync();

        /// <summary>
        /// Adds an item to the shopping cart.
        /// </summary>
        /// <param name="item">The shopping cart item to add.</param>
        /// <returns>The ID of the added shopping cart item.</returns>
        Task<int> AddToCartAsync(ShoppingCartDTO item);

        /// <summary>
        /// Updates the quantity of an item in the shopping cart.
        /// </summary>
        /// <param name="id">The ID of the shopping cart item to update.</param>
        /// <param name="item">The updated shopping cart item.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateCartItemAsync(int id, ShoppingCartDTO item);

        /// <summary>
        /// Removes an item from the shopping cart.
        /// </summary>
        /// <param name="id">The ID of the shopping cart item to remove.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task RemoveFromCartAsync(int id);
    }
}
