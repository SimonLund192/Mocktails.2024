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


}
