using Microsoft.AspNetCore.Mvc;

namespace Mocktails.Website.Controllers;
public class OrderConfirmation : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
