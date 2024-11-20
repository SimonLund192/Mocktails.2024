using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses;
public interface IUserDAO
{
    Task<IEnumerable<User>> GetUserAsync();
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<int> CreateUserAsync(User entity);
    Task<bool> UpdateUserAsync(User entity);
    Task<bool> DeleteUserAsync(int id);
    Task<User> GetUserByIdAsync(int id);
}
