using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Mocktails.DAL.DaoClasses;
using Mocktails.WebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register Swagger services for API documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mocktails API", Version = "v1" });
});

const string connectionString = "Data Source=.;Initial Catalog=MocktailsDB;Integrated Security=True;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true;";
builder.Services.AddSingleton<IMocktailDAO>((_) => new MocktailDAO(connectionString));
builder.Services.AddSingleton<ICategoryDAO>((_) => new CategoryDAO(connectionString));
builder.Services.AddSingleton<IUserDAO>((_) => new UserDAO(connectionString));


// Add DbContext for Entity Framework (MocktailsDbContext)
builder.Services.AddDbContext<MocktailsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MocktailsDb")));

var app = builder.Build();

// Enable Swagger in the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // This enables the Swagger middleware
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mocktails API v1");
    });
}

// Add remaining middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
