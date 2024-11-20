using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNet.Identity;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses;
public class UserDAO : BaseDAO, IUserDAO
{
    private readonly PasswordHasher _passwordHasher;
    public UserDAO(string connectionString) : base(connectionString) 
    {
        _passwordHasher = new PasswordHasher();
    }
    #region CRUD
    public Task<int> CreateUserAsync(User entity)
    {

        throw new NotImplementedException();
        //try
        //{
        //    // Hash the user's password
        //    var hashedPassword = _passwordHasher.HashPassword(entity, entity.PasswordHash);

        //    // Now set the PasswordHash property to the hashed password
        //    entity.PasswordHash = hashedPassword;

        //    var query = "INSERT INTO Users (FirstName, LastName, Email, PasswordHash) VALUES (@FirstName, @LastName, @Email, @PasswordHash); SELECT CAST(SCOPE_IDENTITY() AS INT);";

        //    using var connection = CreateConnection();
        //    var userId = await connection.QuerySingleAsync<int>(query, new { entity.FirstName, entity.LastName, entity.Email, entity.PasswordHash });

        //    return userId;
        //}
        //catch (Exception ex)
        //{
        //    throw new Exception($"Error creating user: {ex.Message}", ex);
        //}


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
    #endregion
}
