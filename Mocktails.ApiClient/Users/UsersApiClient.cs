using Mocktails.ApiClient.Users.DTOs;
using RestSharp;

namespace Mocktails.ApiClient.Users;

public class UsersApiClient : IUsersApiClient
{
    private readonly RestClient _restClient;

    public UsersApiClient(string baseUrl)
    {
        _restClient = new RestClient(baseUrl);
    }

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

    public async Task<bool> DeleteUserAsync(int id)
    {
        var request = new RestRequest($"/api/v1/users/{id}", Method.Delete);
        var response = await _restClient.ExecuteAsync(request);
        return response.IsSuccessful;
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        var request = new RestRequest("/api/v1/users", Method.Get);
        var response = await _restClient.ExecuteAsync<List<UserDTO>>(request);
        return response.Data ?? new List<UserDTO>();
    }

    public async Task<IEnumerable<UserDTO>> GetUserByPartOfNameAsync(string partOfName)
    {
        var request = new RestRequest("/api/v1/users", Method.Get);
        request.AddQueryParameter("partOfName", partOfName);

        var response = await _restClient.ExecuteAsync<List<UserDTO>>(request);

        if (response.IsSuccessful)
        {
            return response.Data ?? new List<UserDTO>();
        }
        else
        {
            throw new Exception($"Failed to retrieve users: {response.ErrorMessage}");
        }
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

    public async Task<bool> UpdateUserAsync(UserDTO entity)
    {
        var request = new RestRequest($"/api/v1/users/{entity.Id}", Method.Put);
        request.AddJsonBody(entity);

        var response = await _restClient.ExecuteAsync(request);
        return response.IsSuccessful;
    }

    public async Task<int> LoginAsync(UserDTO user)
    {
        var request = new RestRequest("/api/v1/users/login", Method.Post);
        request.AddJsonBody(user);

        // Execute the request and expect a JSON object with message and userId
        var response = await _restClient.ExecuteAsync<LoginResponse>(request);

        if (response.IsSuccessful && response.Data != null)
        {
            // Return the user ID from the response
            return response.Data.UserId;
        }
        else
        {
            // Detailed exception message for debugging
            throw new Exception($"Failed to login: {response.Content ?? response.ErrorMessage}, " +
                                $"Status Code: {response.StatusCode}");
        }
    }
}

public class LoginResponse
{
    public string Message { get; set; }
    public int UserId { get; set; }
}
