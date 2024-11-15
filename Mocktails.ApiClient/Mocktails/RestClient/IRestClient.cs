using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.ApiClient.Mocktails.DTOs;

namespace Mocktails.ApiClient.Mocktails.RestClient;
public interface IRestClient
{
    Task<IEnumerable<MocktailDTO>> GetMocktailsAsync();
    Task<IEnumerable<MocktailDTO>> GetTenLatestMocktailsAsync();
    Task<int> CreateMocktailAsync(MocktailDTO entity);
    Task<bool> UpdateMocktailAsync(MocktailDTO entity);
    Task<bool> DeleteMocktailAsync(int id);
    // Get a specific mocktail by ID asynchronously
    Task<MocktailDTO> GetMocktailByIdAsync(int id);
    

}
