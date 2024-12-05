using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNet.Identity;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses;
public class ShoppingCartDAO : BaseDAO, IShoppingCartDAO
{
    public ShoppingCartDAO(string connectionString) : base(connectionString) { }

    public async Task<int> CreateShoppingCartAsync(ShoppingCartItem entity)
    {
        const string query = @"
        INSERT INTO ShoppingCart (SessionId, MocktailId, Quantity, Status)
        OUTPUT INSERTED.Id
        VALUES (@SessionId, @MocktailId, @Quantity, 'Active');
        ";

        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<int>(query, entity);
    }

    public async Task<IEnumerable<ShoppingCartItemWithDetails>> GetCartItemsWithDetailsAsync(string sessionId)
    {
        const string query = @"
        SELECT 
            sc.Id AS CartItemId,
            sc.SessionId,
            sc.Quantity,
            sc.Status,
            sc.CreatedAt,
            sc.UpdatedAt,
            m.Id AS MocktailId,
            m.Name AS MocktailName,
            m.Description AS MocktailDescription,
            m.Price AS MocktailPrice,
            m.ImageUrl AS MocktailImageUrl
        FROM ShoppingCart sc
        INNER JOIN Mocktails m ON sc.MocktailId = m.Id
        WHERE sc.SessionId = @SessionId;
    ";

        using var connection = CreateConnection();
        return await connection.QueryAsync<ShoppingCartItemWithDetails>(query, new { SessionId = sessionId });
    }


    public async Task<IEnumerable<ShoppingCartItem>> GetCartItemsAsync(string sessionId)
    {
        const string query = """
        SELECT sc.Id, sc.SessionId, sc.MocktailId, sc.Quantity, 
               m.Name AS MocktailName, m.Price AS MocktailPrice
        FROM ShoppingCart sc
        INNER JOIN Mocktails m ON sc.MocktailId = m.Id
        WHERE sc.SessionId = @SessionId
    """;

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
