using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;

namespace Mocktails.Test.DAOTests;

[TestFixture]
public class OrderItemDAOTests
{
    private IOrderItemDAO _orderItemDAO;

    [SetUp]
    public void Setup()
    {
        // Use a test database connection string
        var connectionString = "Data Source=hildur.ucn.dk;Initial Catalog=DMA-CSD-S231_10462161;User ID=DMA-CSD-S231_10462161;Password=Password1!;TrustServerCertificate=True;";
        _orderItemDAO = new OrderItemDAO(connectionString);
    }

    [Test]
    public async Task CreateOrderItemAsync_ShouldCreateOrderItemAndReturnId()
    {
        var orderItem = new OrderItem
        {
            OrderId = 1,
            MocktailId = 1,
            Quantity = 2,
        };

        var createdId = await _orderItemDAO.CreateOrderItemAsync(orderItem);
        var createdOrderItem = await _orderItemDAO.GetOrderItemByIdAsync(createdId);

        Assert.That(createdOrderItem, Is.Not.Null);
        Assert.That(createdOrderItem.Quantity, Is.EqualTo(orderItem.Quantity));
    }
}
