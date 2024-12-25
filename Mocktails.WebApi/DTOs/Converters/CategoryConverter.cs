using Mocktails.DAL.Model;
using Mocktails.WebApi.DTOs;

namespace Mocktails.WebApi.Converters;

public static class CategoryConverter
{
    public static CategoryDTO ToDTO(Category category)
    {
        return new CategoryDTO
        {
            Id = category.Id,
            CategoryName = category.CategoryName
        };
    }

    public static Category ToModel(CategoryDTO categoryDTO)
    {
        return new Category
        {
            Id = categoryDTO.Id,
            CategoryName = categoryDTO.CategoryName
        };
    }
}
