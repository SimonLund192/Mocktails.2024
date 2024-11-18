using Microsoft.AspNetCore.Mvc;
using Mocktails.WebApi.DTOs;
using Mocktails.WebApi.Services;
using Mocktails.WebApi.Converters;

namespace Mocktails.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            var categoryDTOs = categories.Select(CategoryConverter.ToDTO);
            return Ok(categoryDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();

            return Ok(CategoryConverter.ToDTO(category));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            var category = CategoryConverter.ToModel(categoryDTO);
            var categoryId = await _categoryService.CreateCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryId }, categoryDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO categoryDTO)
        {
            var category = CategoryConverter.ToModel(categoryDTO);
            category.Id = id;

            var success = await _categoryService.UpdateCategoryAsync(category);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var success = await _categoryService.DeleteCategoryAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
