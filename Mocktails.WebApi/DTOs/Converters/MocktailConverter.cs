using Mocktails.DAL.Model;

namespace Mocktails.WebApi.DTOs.Converters;

public static class MocktailConverter
{
    public static MocktailDTO ToDTO(this Mocktail mocktailToConvert)
    {
        ArgumentNullException.ThrowIfNull(mocktailToConvert);

        var mocktailDTO = new MocktailDTO();
        mocktailToConvert.CopyPropertiesTo(mocktailDTO);
        return mocktailDTO;
    }

    public static Mocktail ToModel(this MocktailDTO mocktailDTOToConvert)
    {
        ArgumentNullException.ThrowIfNull(mocktailDTOToConvert);

        var mocktail = new Mocktail();
        mocktailDTOToConvert.CopyPropertiesTo(mocktail);
        return mocktail;
    }

    public static IEnumerable<MocktailDTO> ToDtos(this IEnumerable<Mocktail> mocktailsToConvert)
    {
        ArgumentNullException.ThrowIfNull(mocktailsToConvert);

        return mocktailsToConvert.Select(mocktail => mocktail.ToDTO());
    }

    public static IEnumerable<Mocktail> ToModels(this IEnumerable<MocktailDTO> mocktailDtosToConvert)
    {
        ArgumentNullException.ThrowIfNull(mocktailDtosToConvert);

        return mocktailDtosToConvert.Select(mocktailDTO => mocktailDTO.ToModel());
    }
}
