using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Mocktails.DAL.DaoClasses;

namespace Mocktails.WebApi.Controllers;
public class CartControllerCookie : Controller
{
    private readonly IMocktailDAO _mocktailDAO;

    public CartControllerCookie(IMocktailDAO mocktailDAO)
    {
        _mocktailDAO = mocktailDAO;
    }


    public IActionResult Index()
    {
        return View(GetCartFromCookie);
    }

    public ActionResult Edit(int id, int quantity)
    {
        var cart = LoadChangeAndSaveCart(cart => cart.ChangeQuantity(new MocktailQuantity(_mocktailDAO.GetMocktailByIdAsync(id), quantity)));
        return View("Index", cart);
    }

    public ActionResult Add(int id, int quantity)
    {
        var cart = LoadChangeAndSaveCart(cart => cart.ChangeQuantity(new MocktailQuantity(_mocktailDAO.GetMocktailByIdAsync(id), quantity)));
        return RedirectToAction("Index", cart);
    }

    public ActionResult Delete(int id)
    {
        var cart = LoadChangeAndSaveCart(cart => cart.DeleteMocktailAsync(id));
        return RedirectToAction("Index", cart);
    }

    public ActionResult EmptyCart()
    {
        var cart = LoadChangeAndSaveCart(cart => cart.EmptyAll());
        return RedirectToAction("Index", cart);
    }

    private void SaveCartToCookie(Cart cart)
    {
        var cookieOptions = new CookieOptions();
        cookieOptions.Expires = DateTime.Now.AddDays(7);
        cookieOptions.Path = "/";
        Response.Cookies.Append("Cart", JsonSerializer.Serialize(cart), cookieOptions);
    }

    private Cart GetCartFromCookie()
    {
        Request.Cookies.TryGetValue("Cart", out string? cookie);
        if (cookie == null) { return new Cart(); }
        return JsonSerializer.Deserialize<Cart>(cookie) ?? new Cart();
    }

    private Cart LoadChangeAndSaveCart(Action<Cart> action)
    {
        Cart cart = GetCartFromCookie();
        action(cart);
        ViewBag.Cart = cart;
        SaveCartToCookie(cart);
        return cart;
    }
}
