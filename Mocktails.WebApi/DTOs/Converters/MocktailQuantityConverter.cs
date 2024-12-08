using Mocktails.ApiClient.Products.DTOs;
using Mocktails.DAL.Model;

namespace Mocktails.WebApi.DTOs.Converters;

public static class MocktailQuantityConverter
{
    public static MocktailQuantityDTO ToDTO(this MocktailQuantity mocktailQuantityToConvert)
    {
        if (mocktailQuantityToConvert == null)
            throw new ArgumentNullException(nameof(mocktailQuantityToConvert), "Mocktail object cannot be null.");

        var mocktailQuantityDTO = new MocktailQuantityDTO();
        mocktailQuantityToConvert.CopyPropertiesTo(mocktailQuantityDTO);
        return mocktailQuantityDTO;
    }

    public static MocktailQuantity ToModel(this MocktailQuantityDTO mocktailQuantityDTOToConvert)
    {
        if (mocktailQuantityDTOToConvert == null)
            throw new ArgumentNullException(nameof(mocktailQuantityDTOToConvert), "MocktailQuantityDTO object cannot be null.");

        var mocktailQuantity = new MocktailQuantity();
        mocktailQuantityDTOToConvert.CopyPropertiesTo(mocktailQuantity);
        return mocktailQuantity;
    }

    public static IEnumerable<MocktailQuantityDTO> ToDtos(this IEnumerable<MocktailQuantity> mocktailQuantitiesToConvert)
    {
        if (mocktailQuantitiesToConvert == null)
            throw new ArgumentNullException(nameof(mocktailQuantitiesToConvert), "MocktailQuantities collection cannot be null.");

        return mocktailQuantitiesToConvert.Select(mocktailQuantity => mocktailQuantity.ToDTO());
    }

    public static IEnumerable<MocktailQuantity> ToModels(this IEnumerable<MocktailQuantityDTO> mocktailQuantityDtosToConvert)
    {
        if (mocktailQuantityDtosToConvert == null)
            throw new ArgumentNullException(nameof(mocktailQuantityDtosToConvert), "MocktailQuantityDtos collection cannot be null.");

        return mocktailQuantityDtosToConvert.Select(mocktailQuantityDTO => mocktailQuantityDTO.ToModel());
    }
}
