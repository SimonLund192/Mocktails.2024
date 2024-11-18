using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Mocktails.ApiClient.Mocktails.RestClient;
using Mocktails.WebApi.Data;
using Mocktails.WebApi.Services;
using Mocktails.DAL.DaoClasses;  // Add the DAO classes

namespace Mocktails.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Register Swagger services for API documentation
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mocktails API", Version = "v1" });
            });

            // Register DAO classes with the container
            builder.Services.AddScoped<IMocktailDAO>(provider =>
                new MocktailDAO(builder.Configuration.GetConnectionString("MocktailsDb")));  // Use your database connection string
            builder.Services.AddScoped<ICategoryDAO>(provider =>
                new CategoryDAO(builder.Configuration.GetConnectionString("MocktailsDb")));  // Use your database connection string

            // Register Service classes
            builder.Services.AddScoped<IMocktailService, MocktailService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

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
        }
    }
}
