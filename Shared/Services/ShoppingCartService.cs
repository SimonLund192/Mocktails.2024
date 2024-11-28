using Microsoft.AspNetCore.Http;

namespace Mocktails.Shared.Services;

public class ShoppingCartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ShoppingCartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetOrCreateSessionId()
    {
        var context = _httpContextAccessor.HttpContext;

        // Check if a session ID already exists in cookies
        if (!context.Request.Cookies.TryGetValue("SessionId", out var sessionId))
        {
            // Generate a new session ID if not found
            sessionId = Guid.NewGuid().ToString();

            // Set the session ID in cookies
            context.Response.Cookies.Append("SessionId", sessionId, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });
        }

        return sessionId;
    }
}
