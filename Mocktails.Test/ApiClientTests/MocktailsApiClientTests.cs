//using Moq;
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Mocktails.ApiClient.Products.DTOs;
//using Mocktails.ApiClient.Products;

//namespace Mocktails.Test.ApiClientTests
//{
//    [TestFixture]
//    public class MocktailsApiClientTests
//    {
//        private Mock<IMocktailApiClient> _mockRestClient;
//        private MocktailsApiClient _mocktailsApiClient;

//        [SetUp]
//        public void SetUp()
//        {
//            // Set up the mock IRestClient
//            _mockRestClient = new Mock<IMocktailApiClient>();

//            // Create the MocktailsApiClient with the mocked IRestClient
//            _mocktailsApiClient = new MocktailsApiClient(_mockRestClient.Object);
//        }

//        // Test for GetMocktailsAsync
//        [Test]
//        public async Task GetMocktailsAsync_ShouldReturnMocktails()
//        {
//            // Arrange
//            var mocktails = new List<MocktailDTO>
//            {
//                new MocktailDTO { Id = 1, Name = "Tropical Breeze", Price = 5.99M },
//                new MocktailDTO { Id = 2, Name = "Berry Blast", Price = 4.99M }
//            };

//            _mockRestClient.Setup(m => m.GetMocktailsAsync()).ReturnsAsync(mocktails);

//            // Act
//            var result = await _mocktailsApiClient.GetMocktailsAsync();

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.IsInstanceOf<IEnumerable<MocktailDTO>>(result);
//            Assert.AreEqual(2, result.Count());
//            Assert.AreEqual("Tropical Breeze", result.First().Name);
//        }

//        // Test for GetMocktailByIdAsync
//        [Test]
//        public async Task GetMocktailByIdAsync_ShouldReturnCorrectMocktail()
//        {
//            // Arrange
//            var mocktail = new MocktailDTO { Id = 1, Name = "Tropical Breeze", Price = 5.99M };
//            _mockRestClient.Setup(m => m.GetMocktailByIdAsync(It.IsAny<int>())).ReturnsAsync(mocktail);

//            // Act
//            var result = await _mocktailsApiClient.GetMocktailByIdAsync(1);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(1, result.Id);
//            Assert.AreEqual("Tropical Breeze", result.Name);
//        }

//        // Test for CreateMocktailAsync
//        [Test]
//        public async Task CreateMocktailAsync_ShouldReturnSuccess()
//        {
//            // Arrange
//            var newMocktail = new MocktailDTO { Id = 1, Name = "Tropical Breeze", Price = 5.99M };
//            _mockRestClient.Setup(m => m.CreateMocktailAsync(It.IsAny<MocktailDTO>())).ReturnsAsync(1);

//            // Act
//            var result = await _mocktailsApiClient.CreateMocktailAsync(newMocktail);

//            // Assert
//            Assert.AreEqual(1, result);  // Assuming the response indicates success (e.g., ID 1)
//        }

//        // Test for UpdateMocktailAsync
//        [Test]
//        public async Task UpdateMocktailAsync_ShouldReturnTrueOnSuccess()
//        {
//            // Arrange
//            var mocktailToUpdate = new MocktailDTO { Id = 1, Name = "Tropical Breeze", Price = 5.99M };
//            _mockRestClient.Setup(m => m.UpdateMocktailAsync(It.IsAny<MocktailDTO>())).ReturnsAsync(true);

//            // Act
//            var result = await _mocktailsApiClient.UpdateMocktailAsync(mocktailToUpdate);

//            // Assert
//            Assert.IsTrue(result);  // Ensure the update operation returns true
//        }

//        // Test for DeleteMocktailAsync
//        [Test]
//        public async Task DeleteMocktailAsync_ShouldReturnTrueOnSuccess()
//        {
//            // Arrange
//            int mocktailId = 1;
//            _mockRestClient.Setup(m => m.DeleteMocktailAsync(It.IsAny<int>())).ReturnsAsync(true);

//            // Act
//            var result = await _mocktailsApiClient.DeleteMocktailAsync(mocktailId);

//            // Assert
//            Assert.IsTrue(result);  // Ensure the delete operation returns true
//        }
//    }
//}
