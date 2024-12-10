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
        var connectionString = "Data Source=hildur.ucn.dk;Initial Catalog=DMA-CSD-S231_10462161;User ID=DMA-CSD-S231_10462161;Password=Password1!;TrustServerCertificate=True;";
        _mocktailDAO = new MocktailDAO(connectionString);
    }

    [Test]
    public async Task RandomTestingToSeeIfItWorksAtAll()
    {
        await _mocktailDAO.GetMocktailsAsync();
    }


}
