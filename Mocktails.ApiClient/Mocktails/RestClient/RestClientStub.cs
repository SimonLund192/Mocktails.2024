using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.ApiClient.Mocktails.DTOs;

namespace Mocktails.ApiClient.Mocktails.RestClient;
public class RestClientStub : IRestClient
{
    private static List<MocktailDTO> _mocktails = new List<MocktailDTO>()
{
    new MocktailDTO(){ Id = 1, Name = "Tropical Breeze", Description = "A refreshing mocktail with pineapple and mango.", Price = 5.99M, ImageUrl = "/Images/MocktailProductImages/tropicalbreeze.jpg" },
    new MocktailDTO(){ Id = 2, Name = "Berry Blast", Description = "A sweet mix of strawberries and blueberries.", Price = 4.99M, ImageUrl = "/Images/MocktailProductImages/berryblast.jpg" },
    new MocktailDTO(){ Id = 3, Name = "Citrus Cooler", Description = "A zesty combination of orange, lime, and mint.", Price = 3.99M, ImageUrl = "/Images/MocktailProductImages/citruscooler.jpg" },
    new MocktailDTO(){ Id = 4, Name = "Mango Tango", Description = "A tropical blend of mango, pineapple, and coconut.", Price = 5.49M, ImageUrl = "/Images/MocktailProductImages/mangotango.jpg" },
    new MocktailDTO(){ Id = 5, Name = "Berry Fizz", Description = "A sparkling mocktail with a burst of fresh berries.", Price = 4.79M, ImageUrl = "/Images/MocktailProductImages/berryfizz.jpg" },
    new MocktailDTO(){ Id = 6, Name = "Lime Light", Description = "A tangy and refreshing lime-based drink with a hint of mint.", Price = 3.49M, ImageUrl = "/Images/MocktailProductImages/limelight.jpg" },
    new MocktailDTO(){ Id = 7, Name = "Peach Paradise", Description = "A smooth blend of peaches and tropical fruits.", Price = 4.99M, ImageUrl = "/Images/MocktailProductImages/peachparadise.jpg" },
    new MocktailDTO(){ Id = 8, Name = "Sunset Squeeze", Description = "A bright orange and pomegranate concoction.", Price = 5.29M, ImageUrl = "/Images/MocktailProductImages/sunsetsqueeze.jpg" },
    new MocktailDTO(){ Id = 9, Name = "Minty Fresh", Description = "A cool minty mocktail with a hint of lemon.", Price = 3.79M, ImageUrl = "/Images/MocktailProductImages/mintyfresh.jpg" },
    new MocktailDTO(){ Id = 10, Name = "Coco Dream", Description = "A creamy coconut-based drink with pineapple and lime.", Price = 6.49M, ImageUrl = "/Images/MocktailProductImages/cocodream.jpg" },
    new MocktailDTO(){ Id = 11, Name = "Watermelon Wave", Description = "A hydrating watermelon and cucumber mocktail.", Price = 4.59M, ImageUrl = "/Images/MocktailProductImages/watermelonwave.jpg" },
    new MocktailDTO(){ Id = 12, Name = "Lemon Spark", Description = "A bright lemon and ginger mocktail with a fizz.", Price = 3.89M, ImageUrl = "/Images/MocktailProductImages/lemonspark.jpg" },
    new MocktailDTO(){ Id = 13, Name = "Strawberry Bliss", Description = "A rich strawberry mocktail with a creamy twist.", Price = 4.29M, ImageUrl = "/Images/MocktailProductImages/strawberrybliss.jpg" },
    new MocktailDTO(){ Id = 14, Name = "Blueberry Breeze", Description = "A refreshing blueberry and lemon mocktail.", Price = 4.99M, ImageUrl = "/Images/MocktailProductImages/blueberrybreeze.jpg" },
    new MocktailDTO(){ Id = 15, Name = "Tropical Punch", Description = "A juicy blend of tropical fruits with a splash of citrus.", Price = 5.19M, ImageUrl = "/Images/MocktailProductImages/tropicalpunch.jpg" },
    new MocktailDTO(){ Id = 16, Name = "Pineapple Splash", Description = "A tangy pineapple mocktail with a citrus zing.", Price = 3.99M, ImageUrl = "/Images/MocktailProductImages/pineapplesplash.jpg" },
    new MocktailDTO(){ Id = 17, Name = "Raspberry Rush", Description = "A tart and sweet mocktail with raspberries and lime.", Price = 4.79M, ImageUrl = "/Images/MocktailProductImages/raspberryrush.jpg" },
    new MocktailDTO(){ Id = 18, Name = "Citrus Sparkle", Description = "A light and fizzy citrus-based mocktail.", Price = 3.89M, ImageUrl = "/Images/MocktailProductImages/citrussparkle.jpg" },
    new MocktailDTO(){ Id = 19, Name = "Cucumber Cooler", Description = "A cool and refreshing cucumber mocktail with a hint of mint.", Price = 4.29M, ImageUrl = "/Images/MocktailProductImages/cucumbercooler.jpg" },
    new MocktailDTO(){ Id = 20, Name = "Apple Orchard", Description = "A crisp apple and cinnamon mocktail with a touch of caramel.", Price = 5.29M, ImageUrl = "/Images/MocktailProductImages/appleorchard.jpg" }
};


    public IEnumerable<MocktailDTO> GetMocktails()
    {
        return _mocktails;
    }

    public MocktailDTO GetMocktailById(int id)
    {
        return _mocktails.FirstOrDefault(mocktail => mocktail.Id == id);
    }
}
