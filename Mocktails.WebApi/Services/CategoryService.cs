using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mocktails.WebApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDAO _categoryDAO;

        public CategoryService(ICategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }

        // Create a new Category
        public async Task<int> CreateCategoryAsync(Category entity)
        {
            try
            {
                return await _categoryDAO.CreateCategoryAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating category: {ex.Message}", ex);
            }
        }

        // Get all Categories
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            try
            {
                return await _categoryDAO.GetCategoriesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting all categories: {ex.Message}", ex);
            }
        }

        // Get a Category by ID
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _categoryDAO.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    throw new Exception("Category not found");
                }
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting category by ID: {ex.Message}", ex);
            }
        }

        // Update a Category
        public async Task<bool> UpdateCategoryAsync(Category entity)
        {
            try
            {
                return await _categoryDAO.UpdateCategoryAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating category: {ex.Message}", ex);
            }
        }

        // Delete a Category by ID
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                return await _categoryDAO.DeleteCategoryAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting category: {ex.Message}", ex);
            }
        }

        public Task<IEnumerable<CategoryDAO>> GetCategoryByPartOfName(string partOfName)
        {
            throw new NotImplementedException();
        }
    }
}
