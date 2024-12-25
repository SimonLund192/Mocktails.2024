using Mocktails.ApiClient.Products.DTOs;

namespace Mocktails.ApiClient.Products;

public interface IMocktailApiClient
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<MocktailDTO>> GetMocktailsAsync();
    Task<IEnumerable<MocktailDTO>> GetTenLatestMocktailsAsync();
    Task<int> CreateMocktailAsync(MocktailDTO entity);
    Task<bool> UpdateMocktailAsync(MocktailDTO entity);
    Task<bool> DeleteMocktailAsync(int id);
    // Get a specific mocktail by ID asynchronously
    Task<MocktailDTO> GetMocktailByIdAsync(int id);
    Task<IEnumerable<MocktailDTO>> GetMocktailByPartOfNameOrDescription(string partOfNameOrDescription);

    Task<bool> UpdateMocktailQuantityAsync(int id, int quantity);
}
