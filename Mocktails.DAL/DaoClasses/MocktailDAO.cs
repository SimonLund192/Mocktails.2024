using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses
{
    public class MocktailDAO : BaseDAO, IMocktailDAO
    {
        // Constructor to initialize the connection string
        public MocktailDAO(string connectionString) : base(connectionString) { }

        // Create a new Mocktail
        public async Task<int> CreateMocktailAsync(Mocktail entity)
        {
            try
            {
                // SQL query to insert a new mocktail into the Mocktail table (without the Category field)
                var query = "INSERT INTO Mocktails (Name, Description, Price, ImageUrl) OUTPUT INSERTED.Id VALUES (@Name, @Description, @Price, @ImageUrl);";

                // Create connection and execute the query
                using var connection = CreateConnection();
                return (await connection.QuerySingleAsync<int>(query, entity));
            }
            catch (Exception ex)
            {
                // Handle any exceptions during the insertion process
                throw new Exception($"Error inserting new Mocktails: '{ex.Message}'.", ex);
            }
        }

        // Delete a Mocktail by its ID
        public async Task<bool> DeleteMocktailAsync(int id)
        {
            try
            {
                // SQL query to delete the mocktail by its ID
                var query = "DELETE FROM Mocktails where Id=@Id";

                // Create connection and execute the query
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
            catch (Exception ex)
            {
                // Handle any exceptions during the deletion process
                throw new Exception($"Error deleting Mocktail with id {id}: '{ex.Message}'.", ex);
            }
        }

        public Task<IEnumerable<Mocktail>> GetCategoryByIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<Mocktail> GetMocktailByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        // Get Mocktails by part of their name or description (this method is not implemented)
        public async Task<IEnumerable<Mocktail>> GetMocktailByPartOfNameOrDescription(string partOfNameOrDescription)
        {
            try
            {
                var query = "SELECT * FROM Mocktails WHERE Name LIKE @partOfNameOrDescription OR Description LIKE @partOfNameOrDescription";
                using var connection = CreateConnection();
                return await connection.QueryAsync<Mocktail>(query, new { partOfNameOrDescription = $"%{partOfNameOrDescription}%" });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting mocktails: '{ex.Message}'.", ex);
            }
        }

        // Get all Mocktails
        public async Task<IEnumerable<Mocktail>> GetMocktailsAsync()
        {
            try
            {
                // SQL query to select all mocktails from the Mocktail table
                var query = "SELECT * FROM Mocktails";

                // Create connection and execute the query
                using var connection = CreateConnection();
                return (await connection.QueryAsync<Mocktail>(query)).ToList();
            }
            catch (Exception ex)
            {
                // Handle any exceptions during the fetch process
                throw new Exception($"Error getting all mocktails: '{ex.Message}'.", ex);
            }
        }

        // Get the 10 latest Mocktails (this method is not implemented)
        public async Task<IEnumerable<Mocktail>> GetTenLatestMocktailsAsync()
        {
            try
            {
                var query = "SELECT top 10 * FROM Mocktail ORDER BY Price DESC";
                using var connection = CreateConnection();
                return await connection.QueryAsync<Mocktail>(query, new { amount = 10 });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting latest mocktails: '{ex.Message}'.", ex);
            }
        }

        // Update a Mocktail (this method is not implemented)
        public async Task<bool> UpdateMocktailAsync(Mocktail entity)
        {
            try
            {
                var query = "UPDATE Mocktail SET Name=@Name, Description=@Description, Price=@Price , ImageUrl=@ImageUrl WHERE Id=@Id;";
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, entity) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating blog post: '{ex.Message}'.", ex);
            }
        }
    }
}
