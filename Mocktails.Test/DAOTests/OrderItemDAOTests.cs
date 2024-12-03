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
        var connectionString = "Server=MSI\\SQLEXPRESS;Database=MocktailsDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;";
        _orderItemDAO = new OrderItemDAO(connectionString);
    }

    //TODO: Figure out how Orders and orderlines should work.
    //Test first hére, to see it in practice before implementing
    [Test]
    public async Task CreateOrderItemAsync_ShouldCreateOrderItemAndReturnOrderItems()
    {
        //var orderItem = new OrderItem
        //{
        //    OrderId = 2,
        //    MocktailId = 2,
        //    Quantity = 2,
        //    Price =
        //}
    }
}
