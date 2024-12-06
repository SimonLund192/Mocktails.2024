using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;

[TestFixture]
public class MocktailDaoTests
{
    private IMocktailDAO _mocktailDAO;
    private IOrderDAO _orderDAO;

    [SetUp]
    public void Setup()
    {
        // Use a test database connection string
        var connectionString = "Server=MSI\\SQLEXPRESS;Database=MocktailsDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;";
        _mocktailDAO = new MocktailDAO(connectionString);
    }

    [Test]
    public async Task RandomTestingToSeeIfItWorksAtAll()
    {
        await _mocktailDAO.GetMocktailsAsync();
    }

    [Test]
    public async Task CreateMocktailAsync_ShouldCreateMocktailAndReturnId()
    {
        // Arrange: Create a new mocktail object with valid test data
        var mocktail = new Mocktail
        {
            Name = "Test Mocktail", // Name of the mocktail
            Description = "A delicious test mocktail.", // Description of the mocktail
            Price = 9.99M, // Price in decimal format
            Quantity = 50, // Initial stock quantity
            ImageUrl = "http://example.com/test.jpg" // URL for the image
        };

        // Act: Call the CreateMocktailAsync method to insert the mocktail into the database
        // and get the ID of the created record
        var createdMocktailId = await _mocktailDAO.CreateMocktailAsync(mocktail);

        // Assert: Check if the returned ID is valid (greater than 0)
        Assert.That(createdMocktailId, Is.GreaterThan(0), "The created mocktail ID should be greater than 0.");

        // Act: Retrieve the created mocktail from the database using the returned ID
        var createdMocktail = await _mocktailDAO.GetMocktailByIdAsync(createdMocktailId);

        // Assert: Verify that the retrieved mocktail matches the data we provided
        Assert.That(createdMocktail, Is.Not.Null, "The mocktail should exist in the database.");
        Assert.That(createdMocktail.Name, Is.EqualTo(mocktail.Name), "The mocktail name should match.");
        Assert.That(createdMocktail.Description, Is.EqualTo(mocktail.Description), "The mocktail description should match.");
        Assert.That(createdMocktail.Price, Is.EqualTo(mocktail.Price), "The mocktail price should match.");
        Assert.That(createdMocktail.Quantity, Is.EqualTo(mocktail.Quantity), "The mocktail quantity should match.");
        Assert.That(createdMocktail.ImageUrl, Is.EqualTo(mocktail.ImageUrl), "The mocktail image URL should match.");
    }

    [Test]
    public async Task PurchaseMocktailAsync_TwoUsersBuyingLastMocktail_OnlyOneSucceeds()
    {
        // Arrange: Insert a mocktail
        var mocktail = new Mocktail
        {
            Name = "Test Mocker",
            Description = "Description",
            Price = 10.00M,
            Quantity = 1,
            ImageUrl = "http://asd.jpg"
        };

        // Act: Call the CreateMocktailAsync method to insert the mocktail into the database
        // and get the ID of the created record
        var mocktailId = await _mocktailDAO.CreateMocktailAsync(mocktail);
        var mocktailFromDb = await _mocktailDAO.GetMocktailByIdAsync(mocktailId);

        // Act: Simulate two users purchasing simultaneously
        var task1 = Task.Run(() =>
            _mocktailDAO.UpdateMocktailQuantityAsync(mocktailId, 1, mocktailFromDb.RowVersion));

        var task2 = Task.Run(() =>
            _mocktailDAO.UpdateMocktailQuantityAsync(mocktailId, 1, mocktailFromDb.RowVersion));

        await Task.WhenAll(task1, task2);

        // Assert: Only one purchase should succeed
        var successCount = new[] { task1.Result, task2.Result }.Count(x => x);
        Assert.That(successCount, Is.EqualTo(1), "Only one purchase should succeed.");

        
        var updatedMocktail = await _mocktailDAO.GetMocktailByIdAsync(mocktailId);
        Assert.That(updatedMocktail.Quantity, Is.EqualTo(0));

    }

    [Test]
    public async Task DeleteMocktailAsync_ShouldRemoveMocktailFromDatabase()
    {
        var mocktail = new Mocktail
        {
            Name = "Mocktail to Delete", // Name of the mocktail
            Description = "This mocktail will be deleted.", // Description of the mocktail
            Price = 7.99M, // Price of the mocktail
            Quantity = 10, // Quantity in stock
            ImageUrl = "http://example.com/delete.jpg" // URL for the image
        };

        // Act: Insert the mocktail into the database
        var createdMocktailId = await _mocktailDAO.CreateMocktailAsync(mocktail);

        // Assert: Verify the mocktail was created
        var createdMocktail = await _mocktailDAO.GetMocktailByIdAsync(createdMocktailId);
        Assert.That(createdMocktail, Is.Not.Null, "The mocktail should exist before deletion.");

        var deleteSuccess = await _mocktailDAO.DeleteMocktailAsync(createdMocktailId);
        
        // Assert: Verify that the delete operation was successful
        Assert.That(deleteSuccess, Is.True, "The delete operation should return true.");


    }
    
}
