using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses
{
    public class ShoppingCartDAO : BaseDAO, IShoppingCartDAO
    {
        public ShoppingCartDAO(string connectionString) : base(connectionString) { }

        public async Task<IEnumerable<ShoppingCartItem>> GetCartItemsWithDetailsAsync()
        {
            const string query = """
            SELECT 
                sc.Id AS Id,
                sc.MocktailId AS MocktailId,
                sc.Quantity AS Quantity,
                sc.CreatedAt AS CreatedAt,
                sc.UpdatedAt AS UpdatedAt,
                m.Name AS MocktailName,
                m.Description AS MocktailDescription,
                m.ImageUrl AS MocktailImageUrl,
                m.Price AS MocktailPrice,
                (sc.Quantity * m.Price) AS TotalPrice
            FROM ShoppingCart sc
            INNER JOIN Mocktails m ON sc.MocktailId = m.Id;
            """;

            using var connection = CreateConnection();
            return await connection.QueryAsync<ShoppingCartItem>(query);
        }

        public async Task<int> AddToCartAsync(ShoppingCartItem item)
        {
            const string query = """
            INSERT INTO ShoppingCart (MocktailId, Quantity, CreatedAt, UpdatedAt)
            OUTPUT INSERTED.Id
            VALUES (@MocktailId, @Quantity, GETDATE(), GETDATE());
            """;

            using var connection = CreateConnection();
            return await connection.QuerySingleAsync<int>(query, item);
        }

        public async Task<bool> UpdateCartItemAsync(ShoppingCartItem item)
        {
            const string query = """
            UPDATE ShoppingCart
            SET Quantity = @Quantity, UpdatedAt = GETDATE()
            WHERE Id = @Id;
            """;

            using var connection = CreateConnection();
            return await connection.ExecuteAsync(query, item) > 0;
        }

        public async Task<bool> RemoveFromCartAsync(int itemId)
        {
            const string query = "DELETE FROM ShoppingCart WHERE Id = @Id";

            using var connection = CreateConnection();
            return await connection.ExecuteAsync(query, new { Id = itemId }) > 0;
        }

        public async Task<bool> ClearCartAsync()
        {
            const string query = "DELETE FROM ShoppingCart";

            using var connection = CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(query);
            return rowsAffected > 0;
        }
    }
}
