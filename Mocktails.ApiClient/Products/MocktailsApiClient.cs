using Mocktails.ApiClient.Products.DTOs;
using RestSharp;

namespace Mocktails.ApiClient.Products
{
    public class MocktailsApiClient : IMocktailApiClient
    {
        private readonly RestClient _restClient;

        public MocktailsApiClient(string baseUrl)
        {
            _restClient = new RestClient(baseUrl);
        }

        // Create a new mocktail asynchronously
        public async Task<int> CreateMocktailAsync(MocktailDTO entity)
        {
            var request = new RestRequest("/api/v1/mocktails", Method.Post); // Adjust the endpoint as needed
            request.AddJsonBody(entity); // Adding the MocktailDTO object as the body of the request

            var response = await _restClient.ExecuteAsync<int>(request); // Assuming the API returns an ID or status code as an integer
            if (response.IsSuccessful)
            {
                return response.Data; // Return the ID or whatever is returned from the API
            }
            else
            {
                throw new Exception($"Failed to create mocktail: {response.ErrorMessage}");
            }
        }

        // Delete a mocktail asynchronously
        public async Task<bool> DeleteMocktailAsync(int id)
        {
            var request = new RestRequest($"/api/v1/mocktails/{id}", Method.Delete); // Use DELETE method
            var response = await _restClient.ExecuteAsync(request);
            return response.IsSuccessful;
        }

        // Get a specific mocktail by ID asynchronously
        public async Task<MocktailDTO> GetMocktailByIdAsync(int id)
        {
            var request = new RestRequest($"/api/v1/mocktails/{id}", Method.Get);
            var response = await _restClient.ExecuteAsync<MocktailDTO>(request);

            if (response.IsSuccessful)
            {
                return response.Data;  // Return a single MocktailDTO object
            }
            else
            {
                throw new Exception($"Failed to get mocktail by ID: {response.ErrorMessage}");
            }
        }

        public async Task<IEnumerable<MocktailDTO>> GetMocktailByPartOfNameOrDescription(string partOfNameOrDescription)
        {
            var request = new RestRequest("/api/v1/mocktails", Method.Get);
            request.AddQueryParameter("partOfNameOrDescription", partOfNameOrDescription);

            var response = await _restClient.ExecuteAsync<List<MocktailDTO>>(request);

            if (response.IsSuccessful)
            {
                return response.Data ?? new List<MocktailDTO>();
            }
            else
            {
                throw new Exception($"Failed to retrieve mocktails: {response.ErrorMessage}");
            }
        }

        //// Get a specific mocktail by ID asynchronously
        //public async Task<MocktailDTO> GetMocktailByIdAsync(int id)
        //{
        //    var request = new RestRequest($"/api/v1/mocktails/{id}", Method.Get);
        //    var response = await _restClient.ExecuteAsync<MocktailDTO>(request);
        //    return response.Data ?? new MocktailDTO();
        //}

        // Get all mocktails asynchronously
        public async Task<IEnumerable<MocktailDTO>> GetMocktailsAsync()
        {
            var request = new RestRequest("/api/v1/mocktails", Method.Get); // Adjust the endpoint as needed
            var response = await _restClient.ExecuteAsync<List<MocktailDTO>>(request);
            return response.Data ?? new List<MocktailDTO>(); // Return an empty list if no data is found
        }

        // Get the latest 10 mocktails asynchronously
        public async Task<IEnumerable<MocktailDTO>> GetTenLatestMocktailsAsync()
        {
            var request = new RestRequest("/api/v1/mocktails/latest", Method.Get); // Assuming the endpoint for the latest mocktails is different
            var response = await _restClient.ExecuteAsync<List<MocktailDTO>>(request);

            if (response.IsSuccessful)
            {
                return response.Data; // Return the latest 10 mocktails
            }
            else
            {
                throw new Exception($"Failed to get the latest mocktails: {response.ErrorMessage}");
            }
        }

        // Update a mocktail asynchronously
        public async Task<bool> UpdateMocktailAsync(MocktailDTO entity)
        {
            var request = new RestRequest($"/api/v1/mocktails/{entity.Id}", Method.Put); // Adjust the endpoint as needed
            request.AddJsonBody(entity); // Add the MocktailDTO as the request body

            var response = await _restClient.ExecuteAsync(request);
            return response.IsSuccessful; // Return true if the update was successful
        }
    }
}
