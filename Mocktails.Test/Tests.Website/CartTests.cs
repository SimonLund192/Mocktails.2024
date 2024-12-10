using NUnit.Framework;
using Mocktails.Website.Models;

namespace Mocktails.Test.Tests.Website
{
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
            var mocktail = new MocktailQuantity { Id = 1, Name = "Mojito", Price = 10.0m, Quantity = 1 };

            // Act
            _cart.ChangeQuantity(mocktail);
            _cart.ChangeQuantity(new MocktailQuantity { Id = 1, Quantity = 2 });

            // Assert
            var cartMocktail = _cart.Products.Find(p => p.Id == 1);
            Assert.That(cartMocktail, Is.Not.Null);
            Assert.That(cartMocktail.Quantity, Is.EqualTo(3));
        }

        [Test]
        public void AddNewMocktailToCart_AddsItem()
        {
            var mocktail = new MocktailQuantity { Id = 1, Name = "Mojito", Price = 10.0m, Quantity = 1 };

            _cart.ChangeQuantity(mocktail);

            var cartMocktail = _cart.Products.Find(p => p.Id == 1);
            Assert.That(cartMocktail.Quantity, Is.EqualTo(1));
        }

        [Test]
        public void RemoveMocktail_RemovesItemFromCart()
        {
            // Arrange
            var mocktail = new MocktailQuantity { Id = 1, Name = "Mojito", Price = 10.0m, Quantity = 1 };
            _cart.ChangeQuantity(mocktail);

            // Act
            _cart.RemoveMocktail(1);

            // Assert
            var cartMocktail = _cart.Products.Find(p => p.Id == 1);
            Assert.That(cartMocktail, Is.Null);
        }

        [Test]
        public void GetTotal_ReturnsCorrectTotal()
        {
            _cart.ChangeQuantity(new MocktailQuantity { Id = 1, Name = "Mojito", Price = 10.0M, Quantity = 2 });
            _cart.ChangeQuantity(new MocktailQuantity { Id = 2, Name = "Piña Colada", Price = 5.0m, Quantity = 2 });

            var total = _cart.GetTotal();

            Assert.That(total, Is.EqualTo(30.0m));
        }

        [Test]
        public void EmptyCart_RemovesAllItems()
        {
            var mocktail = new MocktailQuantity { Id = 1, Name = "Mojito", Price = 10.0m, Quantity = 1 };
            _cart.ChangeQuantity(mocktail);

            _cart.EmptyAll();

            var cartMocktail = _cart.Products.Find(p => p.Id == 1);
            Assert.That(cartMocktail, Is.Null);
        }

        [Test]
        public void IsEmpty_ReturnsFalse_WhenCartHasItems()
        {
            var mocktail = new MocktailQuantity { Id = 2, Name = "Piña Colada", Price = 10.0m, Quantity = 1 };
            _cart.ChangeQuantity(mocktail);

            Assert.That(_cart.IsEmpty, Is.False);
        }


    }
}
