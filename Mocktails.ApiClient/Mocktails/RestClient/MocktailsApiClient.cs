using System.Collections.Generic;
using System.Threading.Tasks;
using Mocktails.ApiClient.Mocktails.DTOs;

namespace Mocktails.ApiClient.Mocktails.RestClient
{
    public class MocktailsApiClient
    {
        private readonly IRestClient _restClient;

        // Constructor injects IRestClient
        public MocktailsApiClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        // Use the IRestClient to get mocktails asynchronously
        public async Task<IEnumerable<MocktailDTO>> GetMocktailsAsync()
        {
            var mocktails = await _restClient.GetMocktailsAsync();  // Use _restClient to call the API
            return mocktails;
        }

        // Get the latest mocktails asynchronously
        public async Task<IEnumerable<MocktailDTO>> GetTenLatestMocktailsAsync()
        {
            var mocktails = await _restClient.GetTenLatestMocktailsAsync();
            return mocktails;
        }

        // Create a new mocktail asynchronously
        public async Task<int> CreateMocktailAsync(MocktailDTO entity)
        {
            var response = await _restClient.CreateMocktailAsync(entity);
            return response; // Assuming the response is an integer or status code
        }

        // Update a mocktail asynchronously
        public async Task<bool> UpdateMocktailAsync(MocktailDTO entity)
        {
            var response = await _restClient.UpdateMocktailAsync(entity);
            return response;
        }

        // Delete a mocktail asynchronously
        public async Task<bool> DeleteMocktailAsync(int id)
        {
            var response = await _restClient.DeleteMocktailAsync(id);
            return response;
        }

        // Get a specific mocktail by ID asynchronously
        public async Task<MocktailDTO> GetMocktailByIdAsync(int id)
        {
            var mocktail = await _restClient.GetMocktailByIdAsync(id); // Use IRestClient to fetch by ID
            return mocktail;
        }
    }
}
