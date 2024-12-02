using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.ApiClient.Users.DTOs;

namespace Mocktails.ApiClient.Users;
public interface IUsersApiClient
{
    Task<IEnumerable<UserDTO>> GetUserByPartOfNameAsync(string partOfName);
    Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    Task<int> CreateUserAsync(UserDTO entity);
    Task<bool> UpdateUserAsync(UserDTO entity);
    Task<bool> DeleteUserAsync(int id);
    Task<UserDTO> GetUserByIdAsync(int id);
    Task<int> LoginAsync(UserDTO user);

}
