using Microsoft.EntityFrameworkCore;
using Mocktails.DAL.Model;

namespace Mocktails.WebApi.Data;

public class MocktailsDbContext : DbContext
{
    public MocktailsDbContext(DbContextOptions<MocktailsDbContext> options) : base(options) { }

    // Defined DbSets for our models
    public DbSet<Mocktail> Mocktails { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }

    // If you want to override any configurations, you can do so in OnModelCreating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Add any additional custom configuration, like relationships, constraints, etc.
    }
}
