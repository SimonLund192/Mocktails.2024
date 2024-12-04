using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses;
public class OrderItemDAO : BaseDAO, IOrderItemDAO
{

    public OrderItemDAO(string connectionString) : base(connectionString) { }

    public async Task<int> CreateOrderItemAsync(OrderItem entity)
    {
        const string fetchPriceQuery = """
            SELECT Price from Mocktails WHERE Id = @MocktailId
            """;

        using var connection = CreateConnection();
        var pricePerUnit = await connection.QuerySingleAsync<decimal>(fetchPriceQuery, new {entity.MocktailId});

        entity.Price = pricePerUnit * entity.Quantity;

        const string insertQuery = """
            INSERT INTO OrderItems
            (OrderId, MocktailId, Quantity, Price)
            OUTPUT INSERTED.Id
            VALUES
            (@OrderId, @MocktailId, @Quantity, @Price)
            """;

        
        return await connection.QuerySingleAsync<int>(insertQuery, entity);
    }

    public async Task<bool> DeleteOrderItemAsync(int id)
    {
        const string query = """
            DELETE FROM OrderItems
            WHERE MocktailId=@Id
            """;

        using var connection = CreateConnection();
        return await connection.ExecuteAsync(query, new { id }) > 0;
    }

    public async Task<OrderItem> GetOrderItemByIdAsync(int id)
    {
        var query = """
            SELECT * FROM OrderItems
            WHERE Id = @Id
            """;

        using var connection = CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<OrderItem>(query, new {Id = id});

        if (result == null)
        {
            throw new Exception("OrderItem not found.");
        }

        return result;
        
    }
    

    public async Task<IEnumerable<OrderItem>> GetOrderItemsFromOrderByOrderIdAsync(int id)
    {
        const string query = """
            SELECT *
            FROM OrderItems
            WHERE OrderId = 1
            """;

        // Create connection and execute the query
        using var connection = CreateConnection();
        return (await connection.QueryAsync<OrderItem>(query)).ToList();
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync()
    {
        const string query = """
            SELECT * FROM OrderItems
            """;

        using var connection = CreateConnection();
        return (await connection.QueryAsync <OrderItem>(query)).ToList();
    }

    public async Task<bool> UpdateOrderItemAsync(OrderItem entity)
    {
        const string query = """
            UPDATE OrderItems
            SET MocktailId = @MocktailId,
            Quantity = @Quantity,
            Price = @Price
            """;

        using var connection = CreateConnection();
        return await connection.ExecuteAsync(query, entity) > 0;
    }

}
