using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Products;
using Mocktails.Website.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Mocktails.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMocktailApiClient _apiClient;

        public HomeController(IMocktailApiClient apiClient)
        {
            _apiClient = apiClient;
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
    }
}
