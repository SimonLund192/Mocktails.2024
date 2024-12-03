using System;
using System.Collections.Generic;
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
        // SQL query to insert a new order into the Orders table
        const string query = """
            INSERT INTO Orders (UserId, OrderDate, TotalAmount, Status, ShippingAddress)
            OUTPUT Inserted.Id
            VALUES(@UserId, @OrderDate, @TotalAmount, @Status, @ShippingAddress);
            """;

        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<int>(query, entity);
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        try
        {
            var query = "SELECT * FROM Orders WHERE Id = @Id";

            using var connection = CreateConnection();
            var result = await connection.QuerySingleOrDefaultAsync<Order>(query, new { Id = id });

            if (result == null)
            {
                throw new Exception("Order not found.");
            }

            return result;
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
