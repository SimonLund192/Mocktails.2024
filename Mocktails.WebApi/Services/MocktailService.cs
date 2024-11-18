using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;
using Mocktails.WebApi.Services;

public class MocktailService : IMocktailService
{
    private readonly IMocktailDAO _mocktailDAO; // Assuming you're using your DAO layer for data fetching.

    public MocktailService(IMocktailDAO mocktailDAO)
    {
        _mocktailDAO = mocktailDAO;
    }

    public async Task<IEnumerable<Mocktail>> GetMocktailsAsync()
    {
        // Fetch all mocktails
        return await _mocktailDAO.GetMocktailsAsync();
    }

    public async Task<IEnumerable<Mocktail>> GetTenLatestMocktailsAsync()
    {
        // Fetch top 10 latest mocktails
        return await _mocktailDAO.GetTenLatestMocktailsAsync();
    }

    public async Task<int> CreateMocktailAsync(Mocktail entity)
    {
        // Create a new mocktail
        return await _mocktailDAO.CreateMocktailAsync(entity);
    }

    public async Task<bool> UpdateMocktailAsync(Mocktail entity)
    {
        // Update existing mocktail
        return await _mocktailDAO.UpdateMocktailAsync(entity);
    }

    public async Task<bool> DeleteMocktailAsync(int id)
    {
        // Delete mocktail by ID
        return await _mocktailDAO.DeleteMocktailAsync(id);
    }

    public async Task<IEnumerable<Mocktail>> GetMocktailByPartOfNameOrDescription(string partOfNameOrDescription)
    {
        // Fetch mocktails that match the part of their name or description
        return await _mocktailDAO.GetMocktailByPartOfNameOrDescription(partOfNameOrDescription);
    }
}
