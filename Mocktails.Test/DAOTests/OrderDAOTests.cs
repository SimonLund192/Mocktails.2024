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
        var connectionString = "Data Source=hildur.ucn.dk;Initial Catalog=DMA-CSD-S231_10462161;User ID=DMA-CSD-S231_10462161;Password=Password1!;TrustServerCertificate=True;";
        _orderDAO = new OrderDAO(connectionString);
    }

    [Test]
    public async Task CreateOrderAsync_ShouldCreateOrderAndReturnId()
    {
        var order = new Order
        {
            UserId = 1,
            OrderDate = DateTime.Now,
            Status = "Pending",
            ShippingAddress = "123 Mocktail Street",
            OrderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    MocktailId = 1,
                    Quantity = 2,
                    Price = 5.99m
                }

            }
        };

        var generatedOrderId = await _orderDAO.CreateOrderAsync(order);

        Assert.That(generatedOrderId, Is.GreaterThan(0));


        var createdOrder = await _orderDAO.GetOrderByIdAsync(generatedOrderId);
        Assert.That(createdOrder, Is.Not.Null, "");
        Assert.That(createdOrder.UserId, Is.EqualTo(order.UserId));
        Assert.That(createdOrder.Status, Is.EqualTo(order.Status));
        Assert.That(createdOrder.ShippingAddress, Is.EqualTo(order.ShippingAddress));
        Assert.That(createdOrder.OrderItems.Count, Is.EqualTo(order.OrderItems.Count));

        for (int i = 0; i < order.OrderItems.Count; i++)
        {
            var expectedItem = order.OrderItems[i];
            var actualItem = order.OrderItems[i];

            Assert.That(actualItem.MocktailId, Is.EqualTo(expectedItem.MocktailId));
            Assert.That(actualItem.Quantity, Is.EqualTo(expectedItem.Quantity));
            Assert.That(actualItem.Price, Is.EqualTo(expectedItem.Price));
        }
    }


}
