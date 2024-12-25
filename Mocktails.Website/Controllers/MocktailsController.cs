using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Products;
using System.Threading.Tasks;

namespace Mocktails.Website.Controllers;

public class MocktailsController : Controller
{
    private readonly IMocktailApiClient _apiClient;

    public MocktailsController(IMocktailApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IActionResult> Index()
    {
        // Get the list of mocktails from the API asynchronously
        var mocktails = await _apiClient.GetMocktailsAsync(); //

        return View(mocktails); //
    }

    // Mark Details method as async
    public async Task<IActionResult> Details(int id)
    {
        var mocktails = await _apiClient.GetMocktailByIdAsync(id);

        if (mocktails == null)
        {
            return NotFound(); // Return 404 if mocktail not found
        }

        // Return the view with the mocktail details
        return View(mocktails);
    }
}
