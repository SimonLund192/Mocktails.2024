using Mocktails.DAL.Model;

namespace Mocktails.WebApi.DTOs.Converters;

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
