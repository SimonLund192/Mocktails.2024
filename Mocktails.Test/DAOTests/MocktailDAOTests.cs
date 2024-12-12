using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;

[TestFixture]
public class MocktailDaoTests
{
    private IMocktailDAO _mocktailDAO;

    [SetUp]
    public void Setup()
    {
        // Use a test database connection string
        var connectionString = "Data Source=hildur.ucn.dk;Initial Catalog=DMA-CSD-S231_10462161;User ID=DMA-CSD-S231_10462161;Password=Password1!;TrustServerCertificate=True;";
        _mocktailDAO = new MocktailDAO(connectionString);
    }
    [Test]
    public void RandomTestingToSeeIfItWorksAtAll()
    {
        _mocktailDAO.GetMocktailsAsync();
    }
    [Test]
    public void CreateMocktail_FindId()
    {
        // Arrange
        var mocktail = new Mocktail { Id = 30, Name = "Piña Colada", Description = "Møj fin", Price = 5.0m, Quantity = 1, ImageUrl = "https:/testimage.jpg" };
        // Act
        _mocktailDAO.CreateMocktailAsync(mocktail);
        // Assert
        Assert.That(mocktail.Id, Is.EqualTo(30));
    }        
    [Test]
    public void SearchMocktailById()
    {
        var mocktail = new Mocktail { Id = 31, Name = "Piña Colada", Description = "Møj fin", Price = 5.0m, Quantity = 1, ImageUrl = "https:/testimage.jpg" };
        _mocktailDAO.CreateMocktailAsync(mocktail);

        _mocktailDAO.GetMocktailByIdAsync(mocktail.Id);
        Assert.That(mocktail.Id, Is.EqualTo(31));
    }
    [Test]
    public async Task UpdateMocktail_WithNewName()
    {
        //var mocktail = new Mocktail { Id = 30, Name = "Piña Colada", Description = "Møj fin", Price = 5.0m, Quantity = 1, ImageUrl = "https:/testimage.jpg" };

        //_mocktailDAO.UpdateMocktailAsync(mocktail);

        //Assert.That(mocktail.Name, Is.EqualTo("Test name"));
    }

}
