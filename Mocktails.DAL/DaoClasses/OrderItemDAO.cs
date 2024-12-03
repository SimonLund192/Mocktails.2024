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
        const string query = """
            INSERT INTO OrderItems
            (OrderId, MocktailId, Quantity, Price)
            OUTPUT INSERTED.Id
            VALUES
            (@OrderId, @MocktailId, @Quantity, @Price)
            """;

        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<int>(query, entity);
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

    public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync()
    {
        const string query = """
            SELECT *
            FROM OrderItems
            """;

        // Create connection and execute the query
        using var connection = CreateConnection();
        return (await connection.QueryAsync<OrderItem>(query)).ToList();
    }

    public Task<bool> UpdateOrderItemAsync(OrderItem entity)
    {
        throw new NotImplementedException();
    }
}
