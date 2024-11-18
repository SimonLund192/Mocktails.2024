using Dapper;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses;

public class MocktailDAO : BaseDAO, IMocktailDAO
{
    // Constructor to initialize the connection string
    public MocktailDAO(string connectionString) : base(connectionString) { }

    public async Task<int> CreateMocktailAsync(Mocktail entity)
    {
        // SQL query to insert a new mocktail into the Mocktail table (without the Category field)
        const string query = """
            INSERT INTO Mocktails (Name, Description, Price, ImageUrl)
            OUTPUT INSERTED.Id VALUES (@Name, @Description, @Price, @ImageUrl);
            """;

        // Create connection and execute the query
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<int>(query, entity);
    }

    // Delete a Mocktail by its ID
    public async Task<bool> DeleteMocktailAsync(int id)
    {
        // SQL query to delete the mocktail by its ID
        const string query = """
            DELETE FROM Mocktails
            WHERE Id=@Id
            """;

        // Create connection and execute the query
        using var connection = CreateConnection();
        return await connection.ExecuteAsync(query, new { id }) > 0;
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
        const string query = """
            SELECT *
            FROM Mocktails
            WHERE Name LIKE @partOfNameOrDescription OR Description LIKE @partOfNameOrDescription
            """;
        using var connection = CreateConnection();
        return await connection.QueryAsync<Mocktail>(query, new { partOfNameOrDescription = $"%{partOfNameOrDescription}%" });
    }

    // Get all Mocktails
    public async Task<IEnumerable<Mocktail>> GetMocktailsAsync()
    {
        // SQL query to select all mocktails from the Mocktail table
        const string query = """
            SELECT *
            FROM Mocktails
            """;

        // Create connection and execute the query
        using var connection = CreateConnection();
        return (await connection.QueryAsync<Mocktail>(query)).ToList();
    }

    // Get the 10 latest Mocktails (this method is not implemented)
    public async Task<IEnumerable<Mocktail>> GetTenLatestMocktailsAsync()
    {
        const string query = """
            SELECT top 10 *
            FROM Mocktail
            ORDER BY Price DESC
            """;
        using var connection = CreateConnection();
        return await connection.QueryAsync<Mocktail>(query, new { amount = 10 });
    }

    // Update a Mocktail (this method is not implemented)
    public async Task<bool> UpdateMocktailAsync(Mocktail entity)
    {
        const string query = """
            UPDATE Mocktail SET Name=@Name, Description=@Description, Price=@Price , ImageUrl=@ImageUrl
            WHERE Id=@Id;
            """;
        using var connection = CreateConnection();
        return await connection.ExecuteAsync(query, entity) > 0;
    }
}
