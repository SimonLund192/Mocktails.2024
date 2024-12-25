using Mocktails.Website.Models;

namespace Mocktails.Test.Tests.Website;

[TestFixture]
internal class CartTests
{
    private Cart _cart;

    [SetUp]
    public void Setup()
    {
        // Initialize an empty cart for each test
        _cart = new Cart();
    }

    [Test]
    public void AddMocktailToCart_IncreasesQuantity_WhenMocktailExists()
    {
        // Arrange
        var mocktail = new MocktailQuantity(1, 1, 10.0m, "Mojito");

        // Act
        _cart.AddOrAdjustProduct(mocktail);
        _cart.AddOrAdjustProduct(new MocktailQuantity(1, 2, 10.0m, "Mojito"));

        // Assert
        var cartMocktail = _cart.Products.Find(p => p.Id == 1);
        Assert.That(cartMocktail, Is.Not.Null);
        Assert.That(cartMocktail.Quantity, Is.EqualTo(3));
    }

    [Test]
    public void AddNewMocktailToCart_AddsItem()
    {
        var mocktail = new MocktailQuantity(1, 1, 10.0m, "Mojito");

        _cart.AddOrAdjustProduct(mocktail);

        var cartMocktail = _cart.Products.Find(p => p.Id == 1);
        Assert.That(cartMocktail.Quantity, Is.EqualTo(1));
    }

    [Test]
    public void RemoveMocktail_RemovesItemFromCart()
    {
        // Arrange
        var mocktail = new MocktailQuantity(1, 1, 10.0m, "Mojito");
        _cart.AddOrAdjustProduct(mocktail);

        // Act
        _cart.RemoveProduct(1);

        // Assert
        var cartMocktail = _cart.Products.Find(p => p.Id == 1);
        Assert.That(cartMocktail, Is.Null);
    }

    [Test]
    public void GetTotal_ReturnsCorrectTotal()
    {
        _cart.AddOrAdjustProduct(new MocktailQuantity(1, 2, 10.0m, "Mojito"));
        _cart.AddOrAdjustProduct(new MocktailQuantity(2, 2, 5.0m, "Piña Colada"));

        var total = _cart.GetTotal();

        Assert.That(total, Is.EqualTo(30.0m));
    }

    [Test]
    public void EmptyCart_RemovesAllItems()
    {
        var mocktail = new MocktailQuantity(1, 1, 10.0m, "Mojito");
        _cart.AddOrAdjustProduct(mocktail);

        _cart.EmptyAll();

        var cartMocktail = _cart.Products.Find(p => p.Id == 1);
        Assert.That(cartMocktail, Is.Null);
    }

    [Test]
    public void IsEmpty_ReturnsFalse_WhenCartHasItems()
    {
        var mocktail = new MocktailQuantity(2, 1, 10.0m, "Piña Colada");
        _cart.AddOrAdjustProduct(mocktail);

        Assert.That(_cart.IsEmpty, Is.False);
    }
}
