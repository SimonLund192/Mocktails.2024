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
        var connectionString = "Server=MSI\\SQLEXPRESS;Database=MocktailsDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;";
        _orderDAO = new OrderDAO(connectionString);
    }

    [Test]
    public async Task CreateOrderAsync_ShouldCreateOrderAndReturnId_WithOrderItems()
    {
        
    }


}
