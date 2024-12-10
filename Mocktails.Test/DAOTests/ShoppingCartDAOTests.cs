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
        var connectionString = "Data Source=hildur.ucn.dk;Initial Catalog=DMA-CSD-S231_10462161;User ID=DMA-CSD-S231_10462161;Password=Password1!;TrustServerCertificate=True;";
        _cartDAO = new ShoppingCartDAO(connectionString);
    }
}
