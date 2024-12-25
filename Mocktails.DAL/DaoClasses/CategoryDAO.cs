using Dapper;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses;

public class CategoryDAO : BaseDAO, ICategoryDAO
{
    // Constructor to initialize the connection string
    public CategoryDAO(string connectionString) : base(connectionString) { }

    public async Task<int> CreateCategoryAsync(Category entity)
    {
        try
        {
            // SQL query to insert a new mocktail into the Mocktail table (without the Category field)
            var query = "INSERT INTO Categories (CategoryName) OUTPUT INSERTED.Id VALUES (@CategoryName);";

            // Create connection and execute the query
            using var connection = CreateConnection();
            return (await connection.QuerySingleAsync<int>(query, entity));
        }
        catch (Exception ex)
        {
            // Handle any exceptions during the insertion process
            throw new Exception($"Error inserting new Categories: '{ex.Message}'.", ex);
        }
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        try
        {
            // SQL query to delete the mocktail by its ID
            var query = "DELETE FROM Categories where Id=@Id";

            // Create connection and execute the query
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(query, new { id }) > 0;
        }
        catch (Exception ex)
        {
            // Handle any exceptions during the deletion process
            throw new Exception($"Error deleting Categories with id {id}: '{ex.Message}'.", ex);
        }
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        try
        {
            // SQL query to select all mocktails from the Mocktail table
            var query = "SELECT * FROM Categories";

            // Create connection and execute the query
            using var connection = CreateConnection();
            return (await connection.QueryAsync<Category>(query)).ToList();
        }
        catch (Exception ex)
        {
            // Handle any exceptions during the fetch process
            throw new Exception($"Error getting all Categories: '{ex.Message}'.", ex);
        }
    }

    public Task<Category> GetCategoryByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CategoryDAO>> GetCategoryByPartOfName(string partOfName)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateCategoryAsync(Category entity)
    {
        try
        {
            var query = "UPDATE Category SET Name=@CategoryName WHERE Id=@Id;";
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(query, entity) > 0;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating Categories: '{ex.Message}'.", ex);
        }
    }
}
