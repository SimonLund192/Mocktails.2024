using System.Text.Json;
using Mocktails.Website.Models;

namespace Mocktails.Website.Services;

public class CartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Cart GetCartFromCookie()
    {
        var context = _httpContextAccessor.HttpContext;

        if (context.Request.Cookies.TryGetValue("Cart", out string? cookie) && cookie != null)
        {
            return JsonSerializer.Deserialize<Cart>(cookie) ?? new Cart();
        }

        return new Cart();
    }

    public void SaveCartToCookie(Cart cart)
    {
        var context = _httpContextAccessor.HttpContext;

        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(7),
            Path = "/"
        };

        context.Response.Cookies.Append("Cart", JsonSerializer.Serialize(cart), cookieOptions);
    }
}

