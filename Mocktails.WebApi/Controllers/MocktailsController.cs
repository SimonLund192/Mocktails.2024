using Microsoft.AspNetCore.Mvc;
using Mocktails.DAL.DaoClasses;
using Mocktails.WebApi.Converters;
using Mocktails.WebApi.DTOs;

[Route("api/v1/[controller]")]
[ApiController]
public class MocktailsController : ControllerBase
{
    private readonly IMocktailDAO _mocktailDAO;

    public MocktailsController(IMocktailDAO mocktailDAO)
    {
        _mocktailDAO = mocktailDAO;
    }

    // GET: api/mocktails
    [HttpGet]
    public async Task<IActionResult> GetMocktails()
    {
        var mocktails = await _mocktailDAO.GetMocktailsAsync();
        return Ok(mocktails);
    }

    // GET: api/mocktails/search?partOfNameOrDescription=orange
    [HttpGet("search")]
    public async Task<IActionResult> GetMocktailsBySearch([FromQuery] string partOfNameOrDescription)
    {
        var mocktails = await _mocktailDAO.GetMocktailByPartOfNameOrDescription(partOfNameOrDescription);
        return Ok(mocktails);
    }

    // GET: api/mocktails/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<MocktailDTO>> GetMocktailByIdAsync(int id)
    {
        var mocktail = await _mocktailDAO.GetMocktailByIdAsync(id);
        if (mocktail == null)
        {
            return NotFound();
        }
        return Ok(mocktail);
    }

    // POST: api/mocktails
    [HttpPost]
    public async Task<IActionResult> CreateMocktail([FromBody] MocktailDTO mocktailDTO)
    {
        var mocktail = MocktailConverter.ToModel(mocktailDTO);
        var mocktailId = await _mocktailDAO.CreateMocktailAsync(mocktail);

        // Return the newly created mocktail's location URL
        return CreatedAtAction(nameof(GetMocktailByIdAsync), new { id = mocktailId }, mocktailDTO);
    }
}
