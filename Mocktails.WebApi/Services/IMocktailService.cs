using Mocktails.DAL.Model;

namespace Mocktails.WebApi.Services;

public interface IMocktailService
{
    Task<IEnumerable<Mocktail>> GetMocktailsAsync(); // Get all mocktails
    Task<IEnumerable<Mocktail>> GetTenLatestMocktailsAsync(); // Get top 10 latest mocktails
    Task<int> CreateMocktailAsync(Mocktail entity); // Create new mocktail
    Task<bool> UpdateMocktailAsync(Mocktail entity); // Update mocktail details
    Task<bool> DeleteMocktailAsync(int id); // Delete mocktail by ID
    Task<IEnumerable<Mocktail>> GetMocktailByPartOfNameOrDescription(string partOfNameOrDescription); // Get mocktails based on part of name or description
}
