using Mocktails.DAL.Model;
using Mocktails.WebApi.DTOs;

namespace Mocktails.WebApi.Converters
{
    public static class MocktailConverter
    {
        public static MocktailDTO ToDTO(Mocktail mocktail)
        {
            return new MocktailDTO
            {
                Id = mocktail.Id,
                Name = mocktail.Name,
                Description = mocktail.Description,
                Price = mocktail.Price,
                ImageUrl = mocktail.ImageUrl
            };
        }

        public static Mocktail ToModel(MocktailDTO mocktailDTO)
        {
            return new Mocktail
            {
                Id = mocktailDTO.Id,
                Name = mocktailDTO.Name,
                Description = mocktailDTO.Description,
                Price = mocktailDTO.Price,
                ImageUrl = mocktailDTO.ImageUrl
            };
        }
    }
}
