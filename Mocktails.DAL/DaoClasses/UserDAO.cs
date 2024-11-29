using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Mocktails.DAL.Authentication;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses;
public class UserDAO : BaseDAO, IUserDAO
{
    private readonly PasswordHasher<User> _passwordHasher;
    public UserDAO(string connectionString) : base(connectionString)
    {
        _passwordHasher = new PasswordHasher<User>();
    }

    #region CRUD

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        const string query = """
            SELECT *
            FROM Users
            """;

        using var connection = CreateConnection();
        return (await connection.QueryAsync<User>(query)).ToList();
    }

    public async Task<int> CreateUserAsync(User entity, string password)
    {
        try
        {
            var query = "INSERT INTO Users(FirstName, LastName, Email, PasswordHash) OUTPUT INSERTED.Id VALUES (@FirstName, @LastName, @Email, @PasswordHash);";
            var passwordHash = BCryptTool.HashPassword(password);
            using var connection = CreateConnection();
            return await connection.QuerySingleAsync<int>(query, new { FirstName = entity.FirstName, LastName = entity.LastName, Email = entity.Email, PasswordHash = passwordHash });
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating new user: '{ex.Message}'.", ex);
        }

        //throw new NotImplementedException();
    }

    //public async Task<int> CreateUserAsync(User entity, string password)
    //{
    //    try
    //    {
    //        // Hash the plain text password before storing it
    //        var hashedPassword = _passwordHasher.HashPassword(entity, entity.PasswordHash);

    //        // Store the hashed password in the entity's PasswordHash property
    //        entity.PasswordHash = hashedPassword;

    //        const string query = @"
    //        INSERT INTO Users 
    //        (FirstName, LastName, Email, PasswordHash) 
    //        VALUES (@FirstName, @LastName, @Email, @PasswordHash);
    //        SELECT CAST(SCOPE_IDENTITY() AS INT);";

    //        using var connection = CreateConnection();
    //        var userId = await connection.QuerySingleAsync<int>(query, new
    //        {
    //            entity.FirstName,
    //            entity.LastName,
    //            entity.Email,
    //            entity.PasswordHash
    //        });

    //        return userId;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception($"Error creating user: {ex.Message}", ex);
    //    }
    //}
    public async Task<bool> UpdateUserAsync(User entity)
    {
        const string query = """
            UPDATE Users
            SET FirstName = @FirstName,
            LastName = @LastName,
            Email = @Email,
            PasswordHash = @PasswordHash;
            """;

        using var connection = CreateConnection();
        return await connection.ExecuteAsync(query, entity) > 0;
    }
    public async Task<bool> DeleteUserAsync(int id)
    {
        const string query = """
            DELETE FROM Users
            WHERE Id = @Id
            """;

        using var connection = CreateConnection();
        return await connection.ExecuteAsync(query, new { id }) > 0;
    }
    public async Task<User> GetUserByIdAsync(int id)
    {
        try
        {
            var query = "SELECT * FROM Users WHERE Id = @Id";

            using var connection = CreateConnection();
            var result = await connection.QuerySingleOrDefaultAsync<User>(query, new { Id = id });

            if (result == null)
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
    public async Task<User> GetUserByEmailAsync(string email)
    {
        try
        {
            var query = "SELECT * FROM Users WHERE Email = @Email";

            using var connection = CreateConnection();
            var result = await connection.QuerySingleOrDefaultAsync<User>(query, new { Email = email });


            if (result == null)
            {
                throw new Exception("User not found.");
            }

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting user by Email: '{ex.Message}'.", ex);
        }
    }
    public async Task<IEnumerable<User>> GetUserByPartOfNameAsync(string partOfName)
    {
        const string query = """
        SELECT *
        FROM Users
        WHERE FirstName LIKE @partOfName
        OR LastName LIKE @partOfName
        """;

        using var connection = CreateConnection();
        return await connection.QueryAsync<User>(query, new { partOfName = $"%{partOfName}%" });
    }

    public async Task<bool> VerifyPasswordAsync(string email, string password)
    {
        var query = "SELECT * FROM Users WHERE Email = @Email";

        using var connection = CreateConnection();
        var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { Email = email });

        if (user == null)
        {
            return false;
        }

        // Verifying the password using PasswordHasher
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        return result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success;
    }

    public Task<bool> UpdatePasswordAsync(string email, string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public async Task<int> LoginAsync(string email, string password)
    {
        try
        {
            var query = "SELECT Id, PasswordHash FROM Users WHERE Email = @Email";
            using var connection = CreateConnection();

            var userTuple = await connection.QueryFirstOrDefaultAsync<UserTuple>(query, new { Email = email });

            if (userTuple != null && BCryptTool.ValidatePassword(password, userTuple.PasswordHash))
            {
                return userTuple.Id;
            }
            return -1;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error logging in for user with email: {email}: '{ex.Message}'.", ex);
        }

        //throw new NotImplementedException();
    }

















    #endregion
}
