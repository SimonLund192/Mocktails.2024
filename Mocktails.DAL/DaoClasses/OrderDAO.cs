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
        const string updateProductStockQuery = """
            UPDATE Mocktails
            SET Quantity = Quantity - @Quantity
            WHERE Id = @MocktailId AND Quantity >= @Quantity;
            """;
        using var connection = CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);

        //Thread.Sleep(8000);

        try
        {
            // Create the order and retrieve its ID
            var orderId = await connection.QuerySingleAsync<int>(insertOrderQuery, entity, transaction);

            // Insert each order item
            foreach (var orderItem in entity.OrderItems)
            {
                orderItem.OrderId = orderId;
                // Update stock
                var rowsAffected = await connection.ExecuteAsync(
                    updateProductStockQuery,
                    new { MocktailId = orderItem.MocktailId, Quantity = orderItem.Quantity },
                    transaction
                );
                if (rowsAffected == 0)
                {
                    // Lars Fy fy
                    // Ingen exception her
                    throw new Exception($"Insufficient stock for Mocktail ID: {orderItem.MocktailId}");
                }
                await connection.ExecuteAsync(insertOrderItemQuery, orderItem, transaction);
            }
            transaction.Commit();
            return orderId;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
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
    public async Task<bool> UpdateOrderAsync(Order entity)
    {
        const string query = """
            UPDATE Orders
            SET Status = @Status;
            """;

        using var connection = CreateConnection();
        return await connection.ExecuteAsync(query, entity) > 0;
    }
    public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
    {
        const string query = """
            UPDATE Orders
            SET Status = @Status
            WHERE Id = @OrderId;
            """;

        using var connection = CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(query, new { OrderId = orderId, Status = status });
        return rowsAffected > 0;
    }
}
