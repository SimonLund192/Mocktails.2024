using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.ApiClient.Orders.DTOs;

namespace Mocktails.ApiClient.Orders;
public interface IOrdersApiClient
{
    Task<int> CreateOrderAsync(OrderDTO entity);
    Task<IEnumerable<OrderDTO>> GetOrdersAsync();
    Task<OrderDTO> GetOrderByIdAsync(int id);
    Task<bool> UpdateOrderAsync(OrderDTO entity);
}
