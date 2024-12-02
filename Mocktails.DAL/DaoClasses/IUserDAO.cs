using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses;
public interface IUserDAO
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<int> CreateUserAsync(User entity, string password);
    Task<bool> UpdateUserAsync(User entity);
    Task<bool> DeleteUserAsync(int id);
    Task<User> GetUserByIdAsync(int id);
    Task<User> GetUserByEmailAsync(string email);

    Task<IEnumerable<User>> GetUserByPartOfNameAsync(string partOfName);

    Task<bool> VerifyPasswordAsync(string email, string password);


    Task<bool> UpdatePasswordAsync(string email, string oldPassword, string newPassword);
    Task<int> LoginAsync(string email, string password);
}
