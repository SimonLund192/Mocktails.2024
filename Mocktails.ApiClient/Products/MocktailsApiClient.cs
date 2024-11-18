using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task<int> CreateMocktailAsync(MocktailDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMocktailAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MocktailDTO> GetMocktailByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        // Get all mocktails
        public async Task<IEnumerable<MocktailDTO>> GetMocktailsAsync()
        {
            var request = new RestRequest("/api/v1/mocktails", Method.Get);
            var response = await _restClient.ExecuteAsync<List<MocktailDTO>>(request);
            return response.Data ?? new List<MocktailDTO>();
        }

        public Task<IEnumerable<MocktailDTO>> GetTenLatestMocktailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMocktailAsync(MocktailDTO entity)
        {
            throw new NotImplementedException();
        }

        //// Get the latest mocktails asynchronously
        //public async Task<IEnumerable<MocktailDTO>> GetTenLatestMocktailsAsync()
        //{
        //    var mocktails = await _restClient.GetTenLatestMocktailsAsync();
        //    return mocktails;
        //}

        //// Create a new mocktail asynchronously
        //public async Task<int> CreateMocktailAsync(MocktailDTO entity)
        //{
        //    var response = await _restClient.CreateMocktailAsync(entity);
        //    return response; // Assuming the response is an integer or status code
        //}

        //// Update a mocktail asynchronously
        //public async Task<bool> UpdateMocktailAsync(MocktailDTO entity)
        //{
        //    var response = await _restClient.UpdateMocktailAsync(entity);
        //    return response;
        //}

        //// Delete a mocktail asynchronously
        //public async Task<bool> DeleteMocktailAsync(int id)
        //{
        //    var response = await _restClient.DeleteMocktailAsync(id);
        //    return response;
        //}

        //// Get a specific mocktail by ID asynchronously
        //public async Task<MocktailDTO> GetMocktailByIdAsync(int id)
        //{
        //    var mocktail = await _restClient.GetMocktailByIdAsync(id); // Use IRestClient to fetch by ID
        //    return mocktail;
        //}
    }
}
