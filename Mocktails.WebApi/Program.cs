using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Mocktails.WebApi.Data;
using Mocktails.DAL.DaoClasses;
using Mocktails.WebApi.Services;

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

            const string connectionString = "Data Source=.;Initial Catalog=BlogSharp;Integrated Security=True";
            builder.Services.AddSingleton<IMocktailDAO>((_) => new MocktailDAO(connectionString));
            builder.Services.AddSingleton<ICategoryDAO>((_) => new CategoryDAO(connectionString));

            //// Register Service classes
            //builder.Services.AddScoped<IMocktailService, MocktailService>();
            //builder.Services.AddScoped<ICategoryService, CategoryService>();

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
