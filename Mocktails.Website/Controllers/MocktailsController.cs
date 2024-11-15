using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Mocktails.RestClient;

namespace Mocktails.Website.Controllers;
public class MocktailsController : Controller
{
    private readonly MocktailsApiClient _apiClient;

    public MocktailsController(MocktailsApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public IActionResult Index()
    {
        // Get the list of mocktails from the API
        var mocktails = _apiClient.GetMocktails();

        // Return the view with the list of mocktails
        return View(mocktails);
    }

    public IActionResult Details(int id)
    {
        // Get the specific mocktail by ID
        var mocktail = _apiClient.GetMocktail(id);

        if (mocktail == null)
        {
            return NotFound();
        }

        // Return the view with the mocktail details
        return View(mocktail);
    }
}

