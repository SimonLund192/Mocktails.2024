using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.ApiClient.Mocktails.DTOs;

namespace Mocktails.ApiClient.Mocktails.RestClient;
    public class MocktailsApiClient
{
    private readonly IRestClient _restClient;

    public MocktailsApiClient(IRestClient restClient)
    {
        _restClient = restClient;
    }

    // Return IEnumerable<MocktailDTO> instead of IEnumerable<Mocktail>
    public IEnumerable<MocktailDTO> GetMocktails()
    {
        return _restClient.GetMocktails();
    }

    // Return MocktailDTO instead of Mocktail
    public MocktailDTO GetMocktail(int id)
    {
        return _restClient.GetMocktailById(id);
    }
}

