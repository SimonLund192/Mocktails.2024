using NUnit.Framework;
using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;
using Mocktails.DAL.Exceptions;
using System;
using System.Threading.Tasks;
using System.Data;

[TestFixture]
public class MocktailDaoTests
{
    private IMocktailDAO _mocktailDAO;

    [SetUp]
    public void Setup()
    {
        // Use a test database connection string
        var connectionString = "Server=MSI\\SQLEXPRESS;Database=MocktailsDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;";
        _mocktailDAO = new MocktailDAO(connectionString);
    }



}
