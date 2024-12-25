using Mocktails.ApiClient.Orders.DTOs;
using RestSharp;

namespace Mocktails.ApiClient.Orders;

public class OrdersApiClient : IOrdersApiClient
{
    private readonly RestClient _restClient;

    public OrdersApiClient(string baseUrl)
    {
        _restClient = new RestClient(baseUrl);
    }
    public async Task<int> CreateOrderAsync(CreateOrderRequest orderRequest)
    {
        var request = new RestRequest("api/v1/orders", Method.Post);
        request.AddJsonBody(orderRequest);

        var response = await _restClient.ExecuteAsync<int>(request);
        if (response.IsSuccessful)
        {
            return response.Data;
        }
        else
        {
            throw new Exception($"Failed to create order: {response.ErrorMessage}. Response: {response.Content}");
        }
    }
    public async Task<IEnumerable<OrderDTO>> GetOrdersAsync()
    {
        var request = new RestRequest("/api/v1/orders", Method.Get);
        var response = await _restClient.ExecuteAsync<List<OrderDTO>>(request);
        return response.Data ?? new List<OrderDTO>();
    }
    public async Task<OrderDTO> GetOrderByIdAsync(int id)
    {
        var request = new RestRequest($"/api/v1/orders/{id}", Method.Get);
        var response = await _restClient.ExecuteAsync<OrderDTO>(request);

        if (response.IsSuccessful)
        {
            return response.Data;
        }
        else
        {
            throw new Exception($"Failed to get order by Id: {response.ErrorMessage}");
        }
    }
    public async Task<bool> UpdateOrderAsync(OrderDTO entity)
    {
        var request = new RestRequest($"/api/v1/orders/{entity.Id}", Method.Put);
        request.AddJsonBody(entity);

        var response = await _restClient.ExecuteAsync(request);
        return response.IsSuccessful;
    }
}
