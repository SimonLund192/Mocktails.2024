using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Products;
using Mocktails.Website.Models;

namespace Mocktails.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMocktailApiClient _apiClient;

        public HomeController(IMocktailApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IActionResult> Search(string query)
        {
            var mocktails = await _apiClient.GetMocktailByPartOfNameOrDescription(query);

            if (mocktails != null && mocktails.Any())
            {
                return RedirectToAction("Details", "Mocktails", new { id = mocktails.First().Id });
            }

            TempData["ErrorMessage"] = "No mocktails found with that name or description";

            return RedirectToAction("Index");
        }

        // Mark Index method as async
        public async Task<IActionResult> Index()
        {
            // Get the list of mocktails from the API asynchronously
            var mocktails = await _apiClient.GetMocktailsAsync(); // Use await for async method

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
}
