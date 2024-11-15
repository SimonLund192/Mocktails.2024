using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Mocktails.RestClient;
using Mocktails.ApiClient.Mocktails.DTOs;
using System.Collections.Generic;
using Mocktails.Website.Models;
using System.Diagnostics;

namespace Mocktails.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MocktailsApiClient _apiClient;  // Add MocktailApiClient

        // Inject MocktailApiClient in the constructor
        public HomeController(ILogger<HomeController> logger, MocktailsApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        // Modify the Index action to fetch mocktails
        public IActionResult Index()
        {
            // Get the list of mocktails from the API
            var mocktails = _apiClient.GetMocktails();

            // Pass the mocktails data to the view
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
