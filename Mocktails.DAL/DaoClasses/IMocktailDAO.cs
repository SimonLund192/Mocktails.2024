using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    Task<bool> UpdateMocktailAsync(Mocktail entity);
    Task<bool> DeleteMocktailAsync(int id);
    Task<IEnumerable<Mocktail>> GetMocktailByPartOfNameOrDescription(string partOfNameOrDescription);

    Task<IEnumerable<Mocktail>> GetCategoryByIdAsync(int categoryId);
    Task<Mocktail> GetMocktailByIdAsync(int id);
}
