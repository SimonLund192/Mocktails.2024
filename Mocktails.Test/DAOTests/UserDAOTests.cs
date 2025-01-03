using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;

namespace Mocktails.Test.DAOTests;

[TestFixture]
public class UserDAOTests
{
    private UserDAO _userDAO;

    [SetUp]
    public void Setup()
    {
        // Use a test database connection string
        var connectionString = "Data Source=hildur.ucn.dk;Initial Catalog=DMA-CSD-S231_10462161;User ID=DMA-CSD-S231_10462161;Password=Password1!;TrustServerCertificate=True;";
        _userDAO = new UserDAO(connectionString);
    }

    [Test]
    public async Task GetAllUsers()
    {
        var users = await _userDAO.GetAllUsersAsync();

        Assert.That(users, Is.Not.Null);
        Assert.That(users, Is.InstanceOf<IEnumerable<User>>());
    }

    [Test]
    public async Task CreateUserAsync_ReturnUserId()
    {
        var randomEmail = $"testuser_{Guid.NewGuid().ToString().Substring(0, 8)}@example.com";


        var user = new User
        {
            FirstName = "TestCreateUserAsync_ReturnUserId",
            LastName = "Test",
            Email = randomEmail,
        };

        var rawPassword = "TestPassword123";

        var generatedUserId = await _userDAO.CreateUserAsync(user, rawPassword);

        Assert.That(generatedUserId, Is.GreaterThan(0));
    }

    [Test]
    public async Task DeleteUserAsync()
    {
        var randomEmail = $"testuser_{Guid.NewGuid().ToString().Substring(0, 8)}@example.com";

        var user = new User
        {
            FirstName = "TestDeleteUserAsync",
            LastName = "Test",
            Email = randomEmail,
        };

        var rawPassword = "TestPassword123";
        var generatedUserId = await _userDAO.CreateUserAsync(user, rawPassword);
        Assert.That(generatedUserId, Is.GreaterThan(0));

        var deleteResult = await _userDAO.DeleteUserAsync(generatedUserId);
        Assert.That(deleteResult, Is.True, "");

        try
        {
            var deletedUser = await _userDAO.GetUserByIdAsync(generatedUserId);
            Assert.Fail("Expected an exception when fetching a deleted mocktail, but none was thrown");
        }
        catch (Exception ex)
        {
            Assert.That(ex.Message, Does.Contain("User not found."), "Expected exception message to contain 'User not found.'");
        }
    }

    [Test]
    public async Task CreateAnd_GetUserByEmailAsync()
    {
        var randomEmail = $"testuser_{Guid.NewGuid().ToString().Substring(0, 8)}@example.com";

        var user = new User
        {
            FirstName = "TestCreateAnd_GetUserByEmail",
            LastName = "Test",
            Email = randomEmail,
        };

        var rawPassword = "TestPassword123";
        var generatedUserId = await _userDAO.CreateUserAsync(user, rawPassword);
        Assert.That(generatedUserId, Is.GreaterThan(0));

        var fetchedUser = await _userDAO.GetUserByEmailAsync(randomEmail);

        Assert.That(fetchedUser, Is.Not.Null);
        Assert.That(fetchedUser.Email, Is.EqualTo(randomEmail));
        Assert.That(fetchedUser.FirstName, Is.EqualTo("TestCreateAnd_GetUserByEmail"));
        Assert.That(fetchedUser.LastName, Is.EqualTo("Test"));
    }

    [Test]
    public async Task LogInAsync_ValidCredentials_ReturnsUserId()
    {
        // Arrange
        var randomEmail = $"testuser_{Guid.NewGuid().ToString().Substring(0, 8)}@example.com";
        var user = new User
        {
            FirstName = "TestLoginAsync_ValidCredentials",
            LastName = "Test",
            Email = randomEmail,
        };
        var rawPassword = "TestPassword123";
        var generatedUserId = await _userDAO.CreateUserAsync(user, rawPassword);
        Assert.That(generatedUserId, Is.GreaterThan(0));
        // Act
        var loggedInUserId = await _userDAO.LoginAsync(randomEmail, rawPassword);
        // Assert
        Assert.That(loggedInUserId, Is.EqualTo(generatedUserId));
    }
}
