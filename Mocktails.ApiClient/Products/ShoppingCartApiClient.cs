using Mocktails.ApiClient.Products.DTOs;
using RestSharp;

namespace Mocktails.ApiClient.Products;

public class ShoppingCartApiClient : IShoppingCartApiClient
{
    private readonly RestClient _restClient;

    public ShoppingCartApiClient(string baseApiUrl)
    {
        _restClient = new RestClient(baseApiUrl);
    }

    public async Task<IEnumerable<ShoppingCartDTO>> GetCartItemsAsync(string sessionId)
    {
        var response = await _restClient.RequestAsync<IEnumerable<ShoppingCartDTO>>(Method.Get, $"shoppingcart/{sessionId}");
        if (!response.IsSuccessful)
        {
            throw new Exception($"Error fetching cart items: {response.Content}");
        }
        return response.Data;
    }

    public async Task<int> AddToCartAsync(ShoppingCartDTO item)
    {
        var response = await _restClient.RequestAsync<int>(Method.Post, "shoppingcart", item);
        if (!response.IsSuccessful)
        {
            throw new Exception($"Error adding item to cart: {response.Content}");
        }
        return response.Data;
    }

    public async Task UpdateCartItemAsync(int id, ShoppingCartDTO item)
    {
        var response = await _restClient.RequestAsync(Method.Put, $"shoppingcart/{id}", item);
        if (!response.IsSuccessful)
        {
            throw new Exception($"Error updating cart item: {response.Content}");
        }
    }

    public async Task RemoveFromCartAsync(int id)
    {
        var response = await _restClient.RequestAsync(Method.Delete, $"shoppingcart/{id}");
        if (!response.IsSuccessful)
        {
            throw new Exception($"Error removing cart item: {response.Content}");
        }
    }
}
