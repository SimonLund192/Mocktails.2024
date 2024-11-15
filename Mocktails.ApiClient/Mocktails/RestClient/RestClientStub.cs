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
            new MocktailDTO(){ Id = 1, Name = "Tropical Breeze", Description = "A refreshing mocktail with pineapple and mango.", Price = 5.99M, ImageUrl = "tropicalbreeze.jpg" },
            new MocktailDTO(){ Id = 2, Name = "Berry Blast", Description = "A sweet mix of strawberries and blueberries.", Price = 4.99M, ImageUrl = "berryblast.jpg" },
            new MocktailDTO(){ Id = 3, Name = "Citrus Cooler", Description = "A zesty combination of orange, lime, and mint.", Price = 3.99M, ImageUrl = "citruscooler.jpg" }
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
