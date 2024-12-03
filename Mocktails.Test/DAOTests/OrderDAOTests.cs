using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public async Task CreateOrderAsync_ShouldCreateOrderAndReturnId()
    {
        // Arrange: Create a new order object with valid test data
        var now = DateTime.Now;
        var truncatedNow = new DateTime(now.Ticks / TimeSpan.TicksPerSecond * TimeSpan.TicksPerSecond); // Truncate to seconds

        var order = new Order
        {
            UserId = 2,
            OrderDate = truncatedNow, // Use truncated time
            TotalAmount = 10,
            Status = "Pending",
            ShippingAddress = "Tambosundvej 43"
        };

        // Act: Call the CreateOrderAsync method to insert the order into the database
        var createdOrderId = await _orderDAO.CreateOrderAsync(order);

        // Assert: Check if the returned Id is valid
        Assert.That(createdOrderId, Is.GreaterThan(0), "The created order ID should be greater than 0.");

        // Act: Retrieve the created order from the database using the returned Id
        var createdOrder = await _orderDAO.GetOrderByIdAsync(createdOrderId);

        // Assert: Verify that the retrieved order matches the provided data
        Assert.That(createdOrder, Is.Not.Null, "The order should exist in the database.");
        Assert.That(createdOrder.UserId, Is.EqualTo(order.UserId), "The user ID should match.");
        Assert.That(createdOrder.OrderDate, Is.EqualTo(order.OrderDate), "The order date should match (to second precision).");
        Assert.That(createdOrder.TotalAmount, Is.EqualTo(order.TotalAmount), "The total amount should match.");
        Assert.That(createdOrder.Status, Is.EqualTo(order.Status), "The status should match.");
        Assert.That(createdOrder.ShippingAddress, Is.EqualTo(order.ShippingAddress), "The shipping address should match.");
    }

}
