using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Products;
using Mocktails.Website.Models;

namespace Mocktails.Website.Controllers;

public class HomeController : Controller
{
    private readonly IMocktailApiClient _apiClient;

    public HomeController(IMocktailApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IActionResult> Index(string? q = null)
    {
        // Get the list of mocktails from the API asynchronously

        var mocktails = string.IsNullOrEmpty(q)
            ? await _apiClient.GetMocktailsAsync()
            : await _apiClient.GetMocktailByPartOfNameOrDescription(q); // Use await for async method

        // Return the view with the list of mocktails
        return View(mocktails);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
