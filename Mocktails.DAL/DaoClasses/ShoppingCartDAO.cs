using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses;
public class ShoppingCartDAO : BaseDAO, IShoppingCartDAO
{
    public ShoppingCartDAO(string connectionString) : base(connectionString) { }

    //public async Task<int> CreateShoppingCartAsync(ShoppingCartItem entity)
    //{
    //    const string query = """
    //        INSERT INTO ShoppingCart (
    //        """
    //}

    public async Task<IEnumerable<ShoppingCartItem>> GetCartItemsAsync(string sessionId)
    {
        var query = "SELECT * FROM ShoppingCart WHERE SessionId = @SessionId";
        using var connection = CreateConnection();
        return await connection.QueryAsync<ShoppingCartItem>(query, new { SessionId = sessionId });
    }

    public async Task<int> AddToCartAsync(ShoppingCartItem item)
    {
        var query = @"INSERT INTO ShoppingCart (SessionId, MocktailId, Quantity)
                          OUTPUT INSERTED.Id
                          VALUES (@SessionId, @MocktailId, @Quantity)";
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<int>(query, item);
    }

    public async Task<bool> UpdateCartItemAsync(ShoppingCartItem item)
    {
        var query = @"UPDATE ShoppingCart SET Quantity = @Quantity, UpdatedAt = GETDATE()
                          WHERE Id = @Id AND SessionId = @SessionId";
        using var connection = CreateConnection();
        return await connection.ExecuteAsync(query, item) > 0;
    }

    public async Task<bool> RemoveFromCartAsync(int itemId)
    {
        var query = "DELETE FROM ShoppingCart WHERE Id = @Id";
        using var connection = CreateConnection();
        return await connection.ExecuteAsync(query, new { Id = itemId }) > 0;
    }
}
