using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses;

public interface IMocktailDAO
{
    Task<IEnumerable<Mocktail>> GetMocktailsAsync();
    Task<IEnumerable<Mocktail>> GetTenLatestMocktailsAsync();
    /// <summary>
    /// Create a new Mocktail
    /// </summary>
    /// <param name="entity">The entity to create</param>
    /// <returns>The entity ID</returns>
    Task<int> CreateMocktailAsync(Mocktail entity);
    /// <summary>
    /// Updates the information on a specific mocktail
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>bool</returns>
    Task<bool> UpdateMocktailAsync(Mocktail entity);
    Task<bool> DeleteMocktailAsync(int id);
    Task<IEnumerable<Mocktail>> GetMocktailByPartOfNameOrDescription(string partOfNameOrDescription);
    Task<IEnumerable<Mocktail>> GetCategoryByIdAsync(int categoryId);
    Task<Mocktail> GetMocktailByIdAsync(int id);
    Task<bool> UpdateMocktailQuantityAsync(int id, int quantity);
}
