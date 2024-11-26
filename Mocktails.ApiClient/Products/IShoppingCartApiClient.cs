using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.ApiClient.Products.DTOs;

namespace Mocktails.ApiClient.Products;
public interface IShoppingCartApiClient
{
    Task<IEnumerable<ShoppingCartDTO>> GetCartItemsAsync(string sessionId);
    Task<int> AddToCartAsync(ShoppingCartDTO item);
    Task UpdateCartItemAsync(int id, ShoppingCartDTO item);
    Task RemoveFromCartAsync(int id);
}
