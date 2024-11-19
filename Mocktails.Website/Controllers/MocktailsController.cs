using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Products;
using System.Threading.Tasks;

namespace Mocktails.Website.Controllers
{
    public class MocktailsController : Controller
    {
        private readonly IMocktailApiClient _apiClient;

        public MocktailsController(IMocktailApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        // Mark Index method as async
        public async Task<IActionResult> Index()
        {
            // Get the list of mocktails from the API asynchronously
            var mocktails = await _apiClient.GetMocktailsAsync(); // Use await for async method

            // Return the view with the list of mocktails
            return View(mocktails); // The view expects an IEnumerable<MocktailDTO> model
        }

        // Mark Details method as async
        public async Task<IActionResult> Details(int id)
        {
            // Get the specific mocktail by ID asynchronously
            var mocktails = await _apiClient.GetMocktailByIdAsync(id); // Use await for async method

            if (mocktails == null)
            {
                return NotFound(); // Return 404 if mocktail not found
            }

            // Return the view with the mocktail details
            return View(mocktails); // The view expects a MocktailDTO model
        }
    }
}
