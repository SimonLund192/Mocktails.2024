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
        
            return Created();

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMocktail([FromRoute]int id, [FromBody] MocktailDTO mocktailDTO)
    {

        if (id != mocktailDTO.Id)
        {
            ModelState.AddModelError(nameof(id), "Id's must match.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var mocktail = MocktailConverter.ToModel(mocktailDTO);
        mocktail.Id = id;

        var success = await _mocktailDAO.UpdateMocktailAsync(mocktail);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMocktail(int id)
    {
        var success = await _mocktailDAO.DeleteMocktailAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpPost("purchase")]
    public async Task<IActionResult> PurchaseMocktail([FromBody] PurchaseDTO purchase)
    {
        try
        {
            var mocktail = await _mocktailDAO.GetMocktailByIdAsync(purchase.MocktailId);

            if (mocktail == null)
                return NotFound("Mocktail not found.");

            if (mocktail.Quantity < purchase.Quantity)
                return BadRequest("Insufficient stock.");

            var success = await _mocktailDAO.UpdateMocktailQuantityAsync(
                purchase.MocktailId,
                purchase.Quantity
            );

            if (!success)
                // TODO: Removed RowVersion - figure out what to do instead
                return Conflict("Something something...MocktailsController ");

            return Ok("Purchase successful.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
