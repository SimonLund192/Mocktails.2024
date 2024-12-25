using Microsoft.AspNetCore.Mvc;
using Mocktails.DAL.DaoClasses;
using Mocktails.WebApi.Converters;
using Mocktails.WebApi.DTOs;

namespace Mocktails.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryDAO _categoryDAO;

    public CategoriesController(ICategoryDAO categoryDAO)
    {
        _categoryDAO = categoryDAO;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryDAO.GetCategoriesAsync();
        var categoryDTOs = categories.Select(CategoryConverter.ToDTO);
        return Ok(categoryDTOs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _categoryDAO.GetCategoryByIdAsync(id);
        if (category == null) return NotFound();

        return Ok(CategoryConverter.ToDTO(category));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
    {
        var category = CategoryConverter.ToModel(categoryDTO);
        var categoryId = await _categoryDAO.CreateCategoryAsync(category);
        return CreatedAtAction(nameof(GetCategoryById), new { id = categoryId }, categoryDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO categoryDTO)
    {
        var category = CategoryConverter.ToModel(categoryDTO);
        category.Id = id;

        var success = await _categoryDAO.UpdateCategoryAsync(category);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var success = await _categoryDAO.DeleteCategoryAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }
}
