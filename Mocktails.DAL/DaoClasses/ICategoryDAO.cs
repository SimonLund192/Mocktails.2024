using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.DAL.Model;

namespace Mocktails.DAL.DaoClasses;
public interface ICategoryDAO
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<int> CreateCategoryAsync(Category entity);
    Task<bool> UpdateCategoryAsync(Category entity);
    Task<bool> DeleteCategoryAsync(int id);
    Task<IEnumerable<CategoryDAO>> GetCategoryByPartOfName(string partOfName);
}
