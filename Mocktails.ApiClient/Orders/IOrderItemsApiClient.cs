using Mocktails.ApiClient.Orders.DTOs;

namespace Mocktails.ApiClient.Orders;

public interface IOrderItemsApiClient
{
    Task<int> CreateOrderItemAsync(OrderItemDTO entity);
    Task<IEnumerable<OrderItemDTO>> GetOrderItemsAsync();
    Task<bool> DeleteOrderItemsAsync(int id);
}
