using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;

namespace Mocktails.Test.DaoTests;

[TestFixture]
public class MocktailDaoTests
{
    private IMocktailDAO _mocktailDAO;

    [SetUp]
    public void Setup()
    {
        // Use a test database connection string
        var connectionString = "Data Source=hildur.ucn.dk;Initial Catalog=DMA-CSD-S231_10462161;User ID=DMA-CSD-S231_10462161;Password=Password1!;TrustServerCertificate=True;";
        _mocktailDAO = new MocktailDAO(connectionString);
    }
    [Test]
    public async Task CreateMocktail_ReturnId()
    {
        var mocktail = new Mocktail
        {
            Name = "Margarita",
            Description = "Refreshing citrus cocktail",
            Price = 12.99m,
            Quantity = 5,
            ImageUrl = "https://example.com/margarita.jpg"
        };

        // Act
        var generatedId = await _mocktailDAO.CreateMocktailAsync(mocktail);

        // Assert
        Assert.That(generatedId, Is.GreaterThan(0));
    }
    [Test]
    public async Task GetMocktailsAsync_ReturnsAllMocktails()
    {
        // Act
        var mocktails = await _mocktailDAO.GetMocktailsAsync();

        // Assert
        Assert.That(mocktails, Is.Not.Null);
        Assert.That(mocktails, Is.InstanceOf<IEnumerable<Mocktail>>());
    }
    [Test]
    public async Task UpdateMocktail_UpdatesMocktailDetails()
    {
        // Arrange
        var mocktail = new Mocktail
        {
            Name = "Old Fashioned",
            Description = "Whiskey-based classic",
            Price = 15.99m,
            Quantity = 10,
            ImageUrl = "https://example.com/oldfashioned.jpg"
        };
        var mocktailId = await _mocktailDAO.CreateMocktailAsync(mocktail);

        mocktail.Id = mocktailId;
        mocktail.Description = "Updated Description";
        mocktail.Price = 18.99m;

        // Act
        var updateResult = await _mocktailDAO.UpdateMocktailAsync(mocktail);
        var updatedMocktail = await _mocktailDAO.GetMocktailByIdAsync(mocktailId);

        // Assert
        Assert.That(updatedMocktail.Id, Is.EqualTo(mocktailId));
        Assert.That(updatedMocktail.Description, Is.EqualTo("Updated Description"));
        Assert.That(updatedMocktail.Price, Is.EqualTo(18.99m));
    }
    [Test]
    public async Task CreateMocktail_DeleteMocktail()
    {
        var mocktail = new Mocktail
        {
            Name = "ChaiTail",
            Description = "Chai latte style mocktail",
            Price = 12.99m,
            Quantity = 5,
            ImageUrl = "https://example.com/chaitail.jpg"
        };

        // Act
        var generatedId = await _mocktailDAO.CreateMocktailAsync(mocktail);
        Assert.That(generatedId, Is.GreaterThan(0), "Mocktail creation should return a valid ID.");

        var deleteResult = await _mocktailDAO.DeleteMocktailAsync(generatedId);
        Assert.That(deleteResult, Is.True, "Mocktail deletion should return true.");

        // Verify that the mocktail no longer exists
        try
        {
            var deletedMocktail = await _mocktailDAO.GetMocktailByIdAsync(generatedId);
            Assert.Fail("Expected an exception when fetching a deleted mocktail, but none was thrown.");
        }
        catch (Exception ex)
        {
            Assert.That(ex.Message, Does.Contain("Mocktail not found."), "Expected exception message to contain 'Mocktail not found.'");
        }
    }
}
