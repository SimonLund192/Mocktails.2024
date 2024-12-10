using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;

namespace Mocktails.Test.DAOTests;

[TestFixture]
public class OrderDAOTests
{
    private IOrderDAO _orderDAO;

    [SetUp]
    public void Setup()
    {
        // Use a test database connection string
        var connectionString = "Data Source=hildur.ucn.dk;Initial Catalog=DMA-CSD-S231_10462161;User ID=DMA-CSD-S231_10462161;Password=Password1!;TrustServerCertificate=True;";
        _orderDAO = new OrderDAO(connectionString);
    }

    [Test]
    public async Task CreateOrderAsync_ShouldCreateOrderAndReturnId_WithOrderItems()
    {
        
    }


}
