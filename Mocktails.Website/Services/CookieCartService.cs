using System.Text.Json;
using Mocktails.Website.Models;

namespace Mocktails.Website.Services;

public class CookieCartService : ICartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookieCartService(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void SaveCart(Cart cart)
    {
        var cookieCart = new CookieCart(cart);

        // Serialize the cart and save it to a cookie
        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(7),
            Path = "/"
        };

        _httpContextAccessor.HttpContext?.Response.Cookies.Append("Cart", JsonSerializer.Serialize(cookieCart), cookieOptions);
    }

    public Cart GetCart()
    {
        // Retrieve the cart from the cookie
        string? cookie = null;
        _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue("Cart", out cookie);

        if (cookie is null)
            return new Cart();

        var cookieCart = JsonSerializer.Deserialize<CookieCart>(cookie) ?? new CookieCart();

        var cart = new Cart();

        foreach (var product in cookieCart.Products)
        {
            cart.AddOrAdjustProduct(new MocktailQuantity()
            {
                Id = product.Id,
                Quantity = product.Quantity,
                Price = product.Price,
                Name = product.Name,
            });
        }

        return cart;
    }

    public Cart LoadChangeAndSaveCart(Action<Cart> action)
    {
        // Load the cart, apply changes, and save it back to the cookie
        var cart = GetCart();
        action(cart);
        SaveCart(cart);

        return cart;
    }
}

/// <summary>
/// Used for cart cookie serialization, since the <see cref="Cart"/> isn't serializable.
/// </summary>
/// <remarks>
/// Has file access modifier, to make it accessible only to this file.
/// </remarks>
file class CookieCart
{
    public CookieCart()
    {
    }

    public CookieCart(Cart cart)
    {
        Products = cart.Products
            .Select(p => new CookieCartProduct(p))
            .ToList();
    }

    public List<CookieCartProduct> Products { get; set; } = [];
}

/// <summary>
/// Used for cart cookie serialization.
/// </summary>
/// <remarks>
/// Has file access modifier, to make it accessible only to this file.
/// </remarks>
file class CookieCartProduct
{
    public CookieCartProduct()
    {
    }

    public CookieCartProduct(MocktailQuantity product)
    {
        Id = product.Id;
        Quantity = product.Quantity;
        Price = product.Price;
        Name = product.Name;
    }

    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? Name { get; set; }
}
