using Microsoft.AspNetCore.Authentication.Cookies;
using Mocktails.ApiClient.Products;
using Mocktails.ApiClient.Users;
using Mocktails.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add authentication services and configure cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Redirect to this path for unauthorized requests
        options.LogoutPath = "/Account/Logout"; // Optional logout path
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Session expiration time
        options.SlidingExpiration = true; // Reset expiration if the user is active
        options.AccessDeniedPath = "/Account/AccessDenied"; // Path to handle 403 Forbidden
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true; // Secure the session cookie
    options.Cookie.IsEssential = true; // Essential for GDPR compliance
});

// Register the correct RestClient implementation
builder.Services.AddSingleton<IMocktailApiClient>((_) =>
{
    // Assuming this is the correct base URL
    return new MocktailsApiClient("https://localhost:7203");
});

// Register UsersApiClient
builder.Services.AddSingleton<IUsersApiClient>((_) => new UsersApiClient("https://localhost:7203"));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Enable authentication and session handling middleware
app.UseRouting();
app.UseSession();
app.UseAuthentication(); // This ensures the app uses authentication
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
