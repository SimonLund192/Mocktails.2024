using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;
using Mocktails.WebApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mocktails.WebApi.Services
{
    public class MocktailService : IMocktailService
    {
        private readonly IMocktailDAO _mocktailDAO; // DAO instance for database operations

        // Constructor injection to get the mocktailDAO
        public MocktailService(IMocktailDAO mocktailDAO)
        {
            _mocktailDAO = mocktailDAO;
        }

        // Get all mocktails from the database
        public async Task<IEnumerable<Mocktail>> GetMocktailsAsync()
        {
            try
            {
                return await _mocktailDAO.GetMocktailsAsync();
            }
            catch (Exception ex)
            {
                // Log the error (you can implement logging)
                throw new Exception("Error fetching mocktails from the database.", ex);
            }
        }

        // Get top 10 latest mocktails based on some criteria (like most recent based on date or price)
        public async Task<IEnumerable<Mocktail>> GetTenLatestMocktailsAsync()
        {
            try
            {
                return await _mocktailDAO.GetTenLatestMocktailsAsync();
            }
            catch (Exception ex)
            {
                // Log the error (you can implement logging)
                throw new Exception("Error fetching latest mocktails from the database.", ex);
            }
        }

        // Create a new mocktail
        public async Task<int> CreateMocktailAsync(Mocktail entity)
        {
            try
            {
                return await _mocktailDAO.CreateMocktailAsync(entity);
            }
            catch (Exception ex)
            {
                // Log the error (you can implement logging)
                throw new Exception("Error creating new mocktail.", ex);
            }
        }

        // Update an existing mocktail
        public async Task<bool> UpdateMocktailAsync(Mocktail entity)
        {
            try
            {
                return await _mocktailDAO.UpdateMocktailAsync(entity);
            }
            catch (Exception ex)
            {
                // Log the error (you can implement logging)
                throw new Exception("Error updating the mocktail.", ex);
            }
        }

        // Delete a mocktail by ID
        public async Task<bool> DeleteMocktailAsync(int id)
        {
            try
            {
                return await _mocktailDAO.DeleteMocktailAsync(id);
            }
            catch (Exception ex)
            {
                // Log the error (you can implement logging)
                throw new Exception($"Error deleting mocktail with id {id}.", ex);
            }
        }

        // Get mocktails that match part of their name or description
        public async Task<IEnumerable<Mocktail>> GetMocktailByPartOfNameOrDescription(string partOfNameOrDescription)
        {
            try
            {
                return await _mocktailDAO.GetMocktailByPartOfNameOrDescription(partOfNameOrDescription);
            }
            catch (Exception ex)
            {
                // Log the error (you can implement logging)
                throw new Exception("Error fetching mocktails by name or description.", ex);
            }
        }
    }
}
