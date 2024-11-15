using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mocktails.ApiClient.Mocktails.DTOs;

namespace Mocktails.ApiClient.Mocktails.RestClient;
public interface IRestClient
{
    IEnumerable<MocktailDTO> GetMocktails();
    MocktailDTO GetMocktailById(int id);
}
