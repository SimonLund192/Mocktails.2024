using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses;
public interface IOrderDAO
{
    Task<int> CreateOrderAsync(Order entity);
    Task<IEnumerable<Order>> GetOrdersAsync();
    Task<Order> GetOrderByIdAsync(int id);
    Task<bool> UpdateOrderAsync(Order entity);
    Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
}
