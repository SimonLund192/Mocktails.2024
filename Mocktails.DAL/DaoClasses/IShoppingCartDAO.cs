using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses;
public interface IShoppingCartDAO
{
    Task<IEnumerable<ShoppingCartItem>> GetCartItemsAsync(string sessionId);
    Task<int> AddToCartAsync(ShoppingCartItem item);
    Task<bool> UpdateCartItemAsync(ShoppingCartItem item);
    Task<bool> RemoveFromCartAsync(int itemId);
    Task<IEnumerable<ShoppingCartItemWithDetails>> GetCartItemsWithDetailsAsync(string sessionid);
}
