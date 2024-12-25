using Mocktails.ApiClient.Orders.DTOs;

namespace Mocktails.ApiClient.Orders;

public interface IOrdersApiClient
{
    Task<int> CreateOrderAsync(CreateOrderRequest orderRequest);
    Task<IEnumerable<OrderDTO>> GetOrdersAsync();
    Task<OrderDTO> GetOrderByIdAsync(int id);
    Task<bool> UpdateOrderAsync(OrderDTO entity);
}
