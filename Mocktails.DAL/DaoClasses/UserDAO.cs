using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses;
public class UserDAO : BaseDAO, IUserDAO
{
    public UserDAO(string connectionString) : base(connectionString) { }

    public Task<int> CreateUserAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetUserAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        try
        {
            var query = "SELECT * FROM Users WHERE Id = @Id";

            using var connection = CreateConnection();
            var result = await connection.QuerySingleOrDefaultAsync<User>(query, new { Id = id });

            if ( result == null)
            {
                throw new Exception("User not found.");
            }

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting user by ID: '{ex.Message}'.", ex);
        }
    }

    public Task<bool> UpdateUserAsync(User entity)
    {
        throw new NotImplementedException();
    }
}
