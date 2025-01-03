using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Users;
using Mocktails.ApiClient.Users.DTOs;

namespace Mocktails.Website.Controllers;

public class AccountController : Controller
{
    private readonly IUsersApiClient _userApiClient;

    public AccountController(IUsersApiClient userApiClient)
    {
        _userApiClient = userApiClient;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View(); // Displays the login form
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] UserDTO loginInfo, [FromQuery] string returnUrl)
    {
        try
        {
            int userId = await _userApiClient.LoginAsync(loginInfo);
            var user = await _userApiClient.GetUserByIdAsync(userId);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Role, "User") // Set user roles as needed
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return string.IsNullOrEmpty(returnUrl) ? RedirectToAction("Index", "Home") : Redirect(returnUrl);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        TempData["SuccessMessage"] = "You have been logged out.";
        return RedirectToAction("Index", "Home");
    }
}
