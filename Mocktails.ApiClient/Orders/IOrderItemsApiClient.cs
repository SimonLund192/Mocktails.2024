using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.ApiClient.Orders.DTOs;

namespace Mocktails.ApiClient.Orders;
public interface IOrderItemsApiClient
{
    Task<int> CreateOrderItemAsync(OrderItemDTO entity);
    Task<IEnumerable<OrderItemDTO>> GetOrderItemsAsync();
    Task<bool> DeleteOrderItemsAsync(int id);
}
