using NUnit.Framework;
using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;
using Mocktails.DAL.Exceptions;
using System;
using System.Threading.Tasks;
using System.Data;

[TestFixture]
public class MocktailDaoTests
{
    private IMocktailDAO _mocktailDAO;

    [SetUp]
    public void Setup()
    {
        // Use a test database connection string
        var connectionString = "Server=MSI\\SQLEXPRESS;Database=MocktailsDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;";
        _mocktailDAO = new MocktailDAO(connectionString);
    }

    //[Test]
    //public async Task PurchaseMocktailAsync_ConcurrencyConflict_ThrowsConcurrencyException()
    //{
    //    // Arrange: Insert a mocktail into the database
    //    var mocktail = new Mocktail
    //    {
    //        Name = "Test Mocktail",
    //        Description = "Test Description",
    //        Price = 5.99M,
    //        Quantity = 10,
    //        ImageUrl = "http://example.com/test.jpg"
    //    };

    //    // Create the mocktail and fetch it back to get the RowVersion
    //    var mocktailId = await _mocktailDAO.CreateMocktailAsync(mocktail);
    //    var mocktailFromDb = await _mocktailDAO.GetMocktailByIdAsync(mocktailId);

    //    // Simulate another process updating the same mocktail
    //    var updatedMocktail = new Mocktail
    //    {
    //        Id = mocktailFromDb.Id,
    //        Name = mocktailFromDb.Name,
    //        Description = mocktailFromDb.Description,
    //        Price = mocktailFromDb.Price,
    //        Quantity = mocktailFromDb.Quantity - 2, // Reduce quantity
    //        ImageUrl = mocktailFromDb.ImageUrl
    //    };
    //    await _mocktailDAO.UpdateMocktailQuantityAsync(updatedMocktail.Id, 2, mocktailFromDb.RowVersion);

    //    // Act & Assert: Attempt to purchase with the old RowVersion
    //    var ex = Assert.ThrowsAsync<ConcurrencyException>(async () =>
    //    {
    //        await _mocktailDAO.UpdateMocktailQuantityAsync(mocktailId, 1, mocktailFromDb.RowVersion);
    //    });

    //    Assert.That(ex.Message, Is.EqualTo("Concurrency conflict occurred."));
    //}

    [Test]
    public async Task PurchaseMocktailAsync_SuccessfulPurchase_UpdatesQuantity()
    {
        // Arrange: Insert a mocktail into the database
        var mocktail = new Mocktail
        {
            Name = "Test Mocktail",
            Description = "Test Description",
            Price = 5.99M,
            Quantity = 10,
            ImageUrl = "http://example.com/test.jpg"
        };

        // Create the mocktail and fetch it back to get the RowVersion
        var mocktailId = await _mocktailDAO.CreateMocktailAsync(mocktail);
        var mocktailFromDb = await _mocktailDAO.GetMocktailByIdAsync(mocktailId);

        // Act: Purchase 2 units using the correct RowVersion
        var success = await _mocktailDAO.UpdateMocktailQuantityAsync(mocktailId, 2, mocktailFromDb.RowVersion);

        // Assert: Ensure the operation was successful and the quantity was updated
        Assert.IsTrue(success);

        var updatedMocktail = await _mocktailDAO.GetMocktailByIdAsync(mocktailId);
        Assert.AreEqual(8, updatedMocktail.Quantity); // 10 - 2 = 8
    }

    [Test]
    public async Task PurchaseMocktailAsync_TwoUsersBuyingLastMocktail_OnlyOneSucceeds()
    {
        // Arrange: Insert a mocktail into the database with only 1 in stock
        var mocktail = new Mocktail
        {
            Name = "Limited Edition Mocktail",
            Description = "Only 1 left!",
            Price = 10.00M,
            Quantity = 1,
            ImageUrl = "http://example.com/limited.jpg"
        };

        // Create the mocktail and fetch it back to get the RowVersion
        var mocktailId = await _mocktailDAO.CreateMocktailAsync(mocktail);
        var mocktailFromDb = await _mocktailDAO.GetMocktailByIdAsync(mocktailId);

        // Define two tasks simulating two users attempting to purchase the last mocktail
        var user1Task = Task.Run(async () =>
        {
            try
            {
                await _mocktailDAO.UpdateMocktailQuantityAsync(mocktailId, 1, mocktailFromDb.RowVersion);
                return true; // Purchase succeeded
            }
            catch (ConcurrencyException)
            {
                return false; // Purchase failed due to concurrency conflict
            }
        });

        var user2Task = Task.Run(async () =>
        {
            try
            {
                await _mocktailDAO.UpdateMocktailQuantityAsync(mocktailId, 1, mocktailFromDb.RowVersion);
                return true; // Purchase succeeded
            }
            catch (ConcurrencyException)
            {
                return false; // Purchase failed due to concurrency conflict
            }
        });

        // Act: Run both tasks simultaneously
        var results = await Task.WhenAll(user1Task, user2Task);

        // Assert: Only one user should succeed
        Assert.That(results.Count(r => r), Is.EqualTo(1), "Only one purchase should succeed.");
        Assert.That(results.Count(r => !r), Is.EqualTo(1), "One purchase should fail due to concurrency conflict.");

        // Verify that the mocktail quantity is reduced to 0
        var updatedMocktail = await _mocktailDAO.GetMocktailByIdAsync(mocktailId);
        Assert.AreEqual(0, updatedMocktail.Quantity, "Mocktail quantity should be 0 after successful purchase.");
    }

}
