//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Mocktails.WebApi.DTOs.Converters;
//using Mocktails.ApiClient.Users;
//using Mocktails.DAL.DaoClasses;
//using Mocktails.DAL.Model;
//using Mocktails.WebApi.DTOs;

//namespace Mocktails.Website.Controllers;
//public class LoginController : Controller
//{
//    private readonly IUsersApiClient _apiClient;

//    public LoginController(IUsersApiClient apiClient)
//    {
//        _apiClient = apiClient;
//    }

//    // GET: Render the login form
//    public IActionResult LoginView()
//    {
//        return View();
//    }

//    // POST: Handle the login process
//    //[HttpPost]
//    //public async Task<IActionResult> Login(LoginDTO loginDTO)
//    //{
//    //    if (!ModelState.IsValid)
//    //    {
//    //        TempData["ErrorMessage"] = "Invalid input. Please check your email and password.";
//    //        return RedirectToAction("LoginView");
//    //    }

//    //    try
//    //    {
//    //        var isLoginSuccessful = await _apiClient.LoginAsync(loginDTO);

//    //        if (isLoginSuccessful)
//    //        {
//    //            TempData["SuccessMessage"] = "Login successful!";
//    //            return RedirectToAction("Index", "Home");
//    //        }
//    //        else
//    //        {
//    //            TempData["ErrorMessage"] = "Invalid email or password.";
//    //            return RedirectToAction("LoginView");
//    //        }
//    //    }
//    //    catch (Exception ex)
//    //    {
//    //        TempData["ErrorMessage"] = $"Error during login: {ex.Message}";
//    //        return RedirectToAction("LoginView");
//    //    }
//    //}
//}
