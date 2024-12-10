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
        // Arrange: Create a new order object with valid test data
        var now = DateTime.Now;
        var truncatedNow = new DateTime(now.Ticks / TimeSpan.TicksPerSecond * TimeSpan.TicksPerSecond); // Truncate to seconds

        var order = new Order
        {
            UserId = 2,
            OrderDate = truncatedNow, // Use truncated time
            Status = "Pending",
            ShippingAddress = "Tambosundvej 43",
            OrderItems = new List<OrderItem>
        {
            new OrderItem { MocktailId = 1, Quantity = 2, Price = 5.50m },
            new OrderItem { MocktailId = 2, Quantity = 1, Price = 3.75m }
        }
        };

        // Act: Call the CreateOrderAsync method to insert the order and its items into the database
        var createdOrderId = await _orderDAO.CreateOrderAsync(order);

        // Assert: Check if the returned order ID is valid
        Assert.That(createdOrderId, Is.GreaterThan(0), "The created order ID should be greater than 0.");

        // Act: Retrieve the created order from the database
        var createdOrder = await _orderDAO.GetOrderByIdAsync(createdOrderId);

        // Assert: Verify the order details
        Assert.That(createdOrder, Is.Not.Null, "The order should exist in the database.");
        Assert.That(createdOrder.UserId, Is.EqualTo(order.UserId), "The user ID should match.");
        Assert.That(createdOrder.OrderDate, Is.EqualTo(order.OrderDate), "The order date should match (to second precision).");
        Assert.That(createdOrder.TotalAmount, Is.EqualTo(14.75m), "The total amount should match."); // 2 * 5.50 + 1 * 3.75
        Assert.That(createdOrder.Status, Is.EqualTo(order.Status), "The status should match.");
        Assert.That(createdOrder.ShippingAddress, Is.EqualTo(order.ShippingAddress), "The shipping address should match.");

        // Verify the order items
        var orderItems = await _orderDAO.GetOrderItemsByOrderIdAsync(createdOrderId);

        Assert.That(orderItems.Count(), Is.EqualTo(order.OrderItems.Count), "The number of order items should match.");
        foreach (var item in order.OrderItems)
        {
            var matchingItem = orderItems.SingleOrDefault(x => x.MocktailId == item.MocktailId && x.Quantity == item.Quantity && x.Price == item.Price);
            Assert.That(matchingItem, Is.Not.Null, $"Order item for MocktailId {item.MocktailId} should exist.");
        }
    }


}
