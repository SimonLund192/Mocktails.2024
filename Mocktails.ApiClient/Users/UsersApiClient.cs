using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.ApiClient.Users.DTOs;
using RestSharp;

namespace Mocktails.ApiClient.Users;
public class UsersApiClient : IUsersApiClient
{

    private readonly RestClient _restClient;

    public async Task<int> CreateUserAsync(UserDTO entity)
    {
        var request = new RestRequest("/api/v1/users", Method.Post);
        request.AddJsonBody(entity);

        var response = await _restClient.ExecuteAsync<int>(request);
        if (response.IsSuccessful)
        {
            return response.Data;
        }
        else
        {
            throw new Exception($"Failed to create user: {response.ErrorMessage}");
        }
    }

    public Task<bool> DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserDTO>> GetUserAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<UserDTO> GetUserByIdAsync(int id)
    {
        var request = new RestRequest($"/api/v1/users/{id}", Method.Get);
        var response = await _restClient.ExecuteAsync<UserDTO>(request);

        if (response.IsSuccessful)
        {
            return response.Data;
        }
        else
        {
            throw new Exception($"Failed to get user by Id: {response.ErrorMessage}");
        }
    }

    public Task<bool> UpdateUserAsync(UserDTO entity)   
    {
        throw new NotImplementedException();
    }
}
