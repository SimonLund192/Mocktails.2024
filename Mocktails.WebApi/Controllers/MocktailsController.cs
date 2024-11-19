using Microsoft.AspNetCore.Mvc;
using Mocktails.DAL.DaoClasses;
using Mocktails.WebApi.DTOs;

namespace Mocktails.WebApi.Controllers
{
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

        [HttpGet("{id}")]
        public async Task<ActionResult<MocktailDTO>> GetMocktailByIdAsync(int id)
        {
            var mocktail = await _mocktailDAO.GetMocktailByIdAsync(id); // Assuming this service call works
            if (mocktail == null)
            {
                return NotFound();
            }
            return Ok(mocktail);
        }
        // Other methods for POST, PUT, DELETE...
    }

}

