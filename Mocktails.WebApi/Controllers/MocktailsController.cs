using Microsoft.AspNetCore.Mvc;
using Mocktails.WebApi.DTOs;
using Mocktails.WebApi.Services;

namespace Mocktails.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MocktailsController : ControllerBase
    {
        private readonly IMocktailService _mocktailService;

        public MocktailsController(IMocktailService mocktailService)
        {
            _mocktailService = mocktailService;
        }

        // GET: api/mocktails
        [HttpGet]
        public async Task<IActionResult> GetMocktails()
        {
            var mocktails = await _mocktailService.GetMocktailsAsync();
            return Ok(mocktails);
        }

        // GET: api/mocktails/search?partOfNameOrDescription=orange
        [HttpGet("search")]
        public async Task<IActionResult> GetMocktailsBySearch([FromQuery] string partOfNameOrDescription)
        {
            var mocktails = await _mocktailService.GetMocktailByPartOfNameOrDescription(partOfNameOrDescription);
            return Ok(mocktails);
        }

        // Other methods for POST, PUT, DELETE...
    }

}

