using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Users;
using Mocktails.ApiClient.Users.DTOs;

namespace Mocktails.Website.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsersApiClient _userApiClient;

        public UserController(IUsersApiClient usersApiClient)
        {
            _userApiClient = usersApiClient;
        }

        // Login GET action: Show the login page
        [HttpGet]
        public IActionResult Login()
        {
            return View(); // This view will contain your login form
        }

        // Login POST action: Handle login form submission
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDTO);
            }

            try
            {
                var response = await _userApiClient.LoginAsync(loginDTO);

                if (response != null)
                {
                    TempData["SuccessMessage"] = "Login successful!";
                    // Redirect to home or user dashboard after login
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid email or password.");
                return View(loginDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing your request.");
                Console.WriteLine(ex.Message);
                return View(loginDTO);
            }
        }

        // Optional: Logout action
        [HttpPost]
        public IActionResult Logout()
        {
            // Clear session or authentication cookie
            TempData["SuccessMessage"] = "You have been logged out.";
            return RedirectToAction("Login");
        }
    }
}
