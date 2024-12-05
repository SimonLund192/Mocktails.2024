using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.DAL.DaoClasses;

namespace Mocktails.Test.DAOTests;
[TestFixture]
public class ShoppingCartDAOTests
{
    private IShoppingCartDAO _cartDAO;

    [SetUp]
    public void Setup()
    {
        // Use a test database connection string
        var connectionString = "Server=MSI\\SQLEXPRESS;Database=MocktailsDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;";
        _cartDAO = new ShoppingCartDAO(connectionString);
    }
}
