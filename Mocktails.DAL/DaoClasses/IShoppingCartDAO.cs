using System.Collections.Generic;
using System.Threading.Tasks;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses
{
    public interface IShoppingCartDAO
    {
        /// <summary>
        /// Retrieves all items in the shopping cart with detailed information.
        /// </summary>
        /// <returns>A list of shopping cart items with details.</returns>
        Task<IEnumerable<ShoppingCartItem>> GetCartItemsWithDetailsAsync();

        /// <summary>
        /// Adds an item to the shopping cart.
        /// </summary>
        /// <param name="item">The shopping cart item to add.</param>
        /// <returns>The ID of the added shopping cart item.</returns>
        Task<int> AddToCartAsync(ShoppingCartItem item);

        /// <summary>
        /// Updates the quantity of an item in the shopping cart.
        /// </summary>
        /// <param name="item">The shopping cart item to update.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        Task<bool> UpdateCartItemAsync(ShoppingCartItem item);

        /// <summary>
        /// Removes an item from the shopping cart.
        /// </summary>
        /// <param name="itemId">The ID of the shopping cart item to remove.</param>
        /// <returns>True if the removal was successful, false otherwise.</returns>
        Task<bool> RemoveFromCartAsync(int itemId);

        /// <summary>
        /// Clears all items from the shopping cart.
        /// </summary>
        /// <returns>True if the operation was successful, false otherwise.</returns>
        Task<bool> ClearCartAsync();
    }
}
