using Microsoft.EntityFrameworkCore;
using Mocktails.WebApi.Services;
using Mocktails.WebApi.Data;

namespace Mocktails.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add DbContext to the DI container
        builder.Services.AddDbContext<MocktailsDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MocktailsDb")));

        // Register services
        builder.Services.AddScoped<IMocktailService, MocktailService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
