using System.Collections.Generic;
using System.Threading.Tasks;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses;

public interface IOrderDAO
{
    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="entity">The order entity to create.</param>
    /// <returns>The ID of the created order.</returns>
    Task<int> CreateOrderAsync(Order entity);

    /// <summary>
    /// Retrieves all orders.
    /// </summary>
    /// <returns>A list of orders.</returns>
    Task<IEnumerable<Order>> GetOrdersAsync();

    /// <summary>
    /// Retrieves a specific order by ID.
    /// </summary>
    /// <param name="id">The ID of the order to retrieve.</param>
    /// <returns>The order entity.</returns>
    Task<Order> GetOrderByIdAsync(int id);

    /// <summary>
    /// Updates an existing order.
    /// </summary>
    /// <param name="entity">The updated order entity.</param>
    /// <returns>True if the update was successful; otherwise, false.</returns>
    Task<bool> UpdateOrderAsync(Order entity);

    /// <summary>
    /// Retrieves all order items associated with a specific order.
    /// </summary>
    /// <param name="orderId">The ID of the order.</param>
    /// <returns>A list of order items.</returns>
    Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
}
