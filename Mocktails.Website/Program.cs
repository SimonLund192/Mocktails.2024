using Microsoft.AspNetCore.Authentication.Cookies;
using Mocktails.ApiClient.Orders;
using Mocktails.ApiClient.Products;
using Mocktails.ApiClient.Users;
using Mocktails.Website.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// Add authentication services and configure cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; 
        options.LogoutPath = "/Account/Logout"; 
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); 
        options.SlidingExpiration = true; 
        options.AccessDeniedPath = "/Account/AccessDenied"; 
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true; 
    options.Cookie.IsEssential = true;
});

// Register the correct RestClient implementation
builder.Services.AddSingleton<IMocktailApiClient>((_) => new MocktailsApiClient("https://localhost:7203"));
builder.Services.AddSingleton<IUsersApiClient>((_) => new UsersApiClient("https://localhost:7203"));
builder.Services.AddSingleton<IOrdersApiClient>((_) => new OrdersApiClient("https://localhost:7203"));

builder.Services.AddSingleton<ICartService, CookieCartService>();

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
