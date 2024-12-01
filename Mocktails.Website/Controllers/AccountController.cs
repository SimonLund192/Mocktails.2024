using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Users;
using Mocktails.ApiClient.Users.DTOs;

namespace Mocktails.Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersApiClient _userApiClient;

        public AccountController(IUsersApiClient usersApiClient)
        {
            _userApiClient = usersApiClient;
        }

        // Login GET action: Show the login page
        [HttpGet]
        public IActionResult Login()
        {
            return View(); // This view will contain your login form
        }

        //[HttpPost]
        //public async Task<IActionResult> Login([FromForm] UserDTO loginInfo, [FromQuery] string returnUrl)
        //{
        //    //TODO: Consider changing the login signature to return the entire author?
        //    int userId = await _userApiClient.LoginAsync(loginInfo);

        //    if (userId > 0)
        //    {
        //        var user = await _userApiClient.GetUserByIdAsync(userId);
        //        var claims = new List<Claim>
        //        {
        //            new Claim("user_id", user.Id.ToString()),
        //            new Claim(ClaimTypes.Email, user.Email),
        //            new Claim(ClaimTypes.Role, "User"),
        //        };

        //        await SignInUsingClaims(claims);
        //        TempData["Message"] = $"You are logged in as {user.Email}";
        //        if (string.IsNullOrEmpty(returnUrl))
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            return Redirect(returnUrl);
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.ErrorMessage = "Incorrect login or user does not exist";
        //    }

        //    return View();
        //}

        // Creates the authentication cookie with claims
        private async Task SignInUsingClaims(List<Claim> claims)
        {
            //Create the container for all your claims
            //These are stored in the cookie for easy retrieval on the server
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                #region often used options - to consider including in cookie
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value. 
                #endregion"
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        // Deletes the authentication cookie
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Message"] = "You are now logged out.";
            return RedirectToAction("Index", "");
        }

        // Displayed if an area is off-limits, based on an authenticated user's claims
        public IActionResult AccessDenied() => View();
    }
}
