using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.ApiClient.Orders.DTOs;
using RestSharp;

namespace Mocktails.ApiClient.Orders;

public class OrderItemsApiClient : IOrderItemsApiClient
{
    private readonly RestClient _restClient;

    public OrderItemsApiClient(string baseUrl)
    {
        _restClient = new RestClient(baseUrl);
    }

    public async Task<int> CreateOrderItemAsync(OrderItemDTO entity)
    {
        var request = new RestRequest("api/v1/orderItems", Method.Post);
        request.AddJsonBody(entity);

        var response = await _restClient.ExecuteAsync<int>(request);
        if(response.IsSuccessful)
        {
            return response.Data;
        }
        else
        {
            throw new Exception($"Failed to create orderItem: {response.ErrorMessage}");
        }
    }

    public async Task<bool> DeleteOrderItemsAsync(int id)
    {
        var request = new RestRequest($"/api/v1/orderItems", Method.Delete);
        var response = await _restClient.ExecuteAsync(request);
        return response.IsSuccessful;
    }

    public async Task<IEnumerable<OrderItemDTO>> GetOrderItemsAsync()
    {
        var request = new RestRequest("/api/v1/orderItems", Method.Get);
        var response = await _restClient.ExecuteAsync<List<OrderItemDTO>>(request);
        return response.Data ?? new List<OrderItemDTO>();
    }
}
