using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;

namespace Mocktails.WebApi.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<int> CreateCategoryAsync(Category entity);
    Task<bool> UpdateCategoryAsync(Category entity);
    Task<bool> DeleteCategoryAsync(int id);
    Task<IEnumerable<CategoryDAO>> GetCategoryByPartOfName(string partOfName);
    Task<Category> GetCategoryByIdAsync(int id);
}
