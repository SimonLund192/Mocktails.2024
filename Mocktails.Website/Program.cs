using Mocktails.ApiClient.Products;
using Mocktails.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the correct RestClient implementation
builder.Services.AddSingleton<IMocktailApiClient>((_) =>
{
    // Assuming this is the correct base URL
    return new MocktailsApiClient("https://localhost:7203");
});

// Register ShoppingCartApiClient
builder.Services.AddSingleton((_) => new ShoppingCartApiClient("https://localhost:7203/api/v1/")); // Replace with your actual Web API URL
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ShoppingCartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
