using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var query = "INSERT INTO Category (CategoryName) OUTPUT INSERTED.Id VALUES (@CategoryName);";

            // Create connection and execute the query
            using var connection = CreateConnection();
            return (await connection.QuerySingleAsync<int>(query, entity));
        }
        catch (Exception ex)
        {
            // Handle any exceptions during the insertion process
            throw new Exception($"Error inserting new Category: '{ex.Message}'.", ex);
        }
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        try
        {
            // SQL query to delete the mocktail by its ID
            var query = "DELETE FROM Category where Id=@Id";

            // Create connection and execute the query
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(query, new { id }) > 0;
        }
        catch (Exception ex)
        {
            // Handle any exceptions during the deletion process
            throw new Exception($"Error deleting Category with id {id}: '{ex.Message}'.", ex);
        }
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        try
        {
            // SQL query to select all mocktails from the Mocktail table
            var query = "SELECT * FROM Category";

            // Create connection and execute the query
            using var connection = CreateConnection();
            return (await connection.QueryAsync<Category>(query)).ToList();
        }
        catch (Exception ex)
        {
            // Handle any exceptions during the fetch process
            throw new Exception($"Error getting all categories: '{ex.Message}'.", ex);
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
            throw new Exception($"Error updating category: '{ex.Message}'.", ex);
        }
    }
}
