using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses;
public class OrderDAO : BaseDAO, IOrderDAO
{
    public OrderDAO(string connectionString) : base(connectionString) { }

    
    public async Task<int> CreateOrderAsync(Order entity)
    {
        // Calculate total amount from OrderItems
        entity.TotalAmount = entity.OrderItems.Sum(item => item.Price * item.Quantity);

        const string insertOrderQuery = """
        INSERT INTO Orders (UserId, OrderDate, TotalAmount, Status, ShippingAddress)
        OUTPUT Inserted.Id
        VALUES(@UserId, @OrderDate, @TotalAmount, @Status, @ShippingAddress);
        """;

        const string insertOrderItemQuery = """
        INSERT INTO OrderItems (OrderId, MocktailId, Quantity, Price)
        VALUES (@OrderId, @MocktailId, @Quantity, @Price);
        """;

        using var connection = CreateConnection();

        // Create the order and retrieve its ID
        var orderId = await connection.QuerySingleAsync<int>(insertOrderQuery, entity);

        // Insert each order item
        foreach (var orderItem in entity.OrderItems)
        {
            orderItem.OrderId = orderId;
            await connection.ExecuteAsync(insertOrderItemQuery, orderItem);
        }

        return orderId;
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
    {
        const string query = """
        SELECT *
        FROM OrderItems
        WHERE OrderId = @OrderId
        """;

        using var connection = CreateConnection();
        return (await connection.QueryAsync<OrderItem>(query, new { OrderId = orderId })).ToList();
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        try
        {
            const string orderQuery = "SELECT * FROM Orders WHERE Id = @Id";
            const string orderItemsQuery = "SELECT * FROM OrderItems WHERE OrderId = @OrderId";

            using var connection = CreateConnection();

            // Retrieve the order
            var order = await connection.QuerySingleOrDefaultAsync<Order>(orderQuery, new { Id = id });
            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            // Retrieve the associated order items
            var orderItems = await connection.QueryAsync<OrderItem>(orderItemsQuery, new { OrderId = id });

            // Attach order items to the order
            order.OrderItems = orderItems.ToList();

            return order;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting order by Id: '{ex.Message}'.", ex);
        }
    }


    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        const string query = """
            SELECT *
            FROM Orders
            """;

        using var connection = CreateConnection();
        return (await connection.QueryAsync<Order>(query)).ToList();
    }

    // UpdateOrderAsync not implemented
    public Task<bool> UpdateOrderAsync(Order entity)
    {
        throw new NotImplementedException();
    }
}
