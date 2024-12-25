using Mocktails.DAL.Model;

namespace Mocktails.WebApi.DTOs.Converters;

public static class MocktailConverter
{
    public static MocktailDTO ToDTO(this Mocktail mocktailToConvert)
    {
        if (mocktailToConvert == null)
            throw new ArgumentNullException(nameof(mocktailToConvert), "Mocktail object cannot be null.");

        var mocktailDTO = new MocktailDTO();
        mocktailToConvert.CopyPropertiesTo(mocktailDTO);
        return mocktailDTO;
    }

    public static Mocktail ToModel(this MocktailDTO mocktailDTOToConvert)
    {
        if (mocktailDTOToConvert == null)
            throw new ArgumentNullException(nameof(mocktailDTOToConvert), "MocktailDTO object cannot be null.");

        var mocktail = new Mocktail();
        mocktailDTOToConvert.CopyPropertiesTo(mocktail);
        return mocktail;
    }

    public static IEnumerable<MocktailDTO> ToDtos(this IEnumerable<Mocktail> mocktailsToConvert)
    {
        if (mocktailsToConvert == null)
            throw new ArgumentNullException(nameof(mocktailsToConvert), "Mocktails collection cannot be null.");

        return mocktailsToConvert.Select(mocktail => mocktail.ToDTO());
    }

    public static IEnumerable<Mocktail> ToModels(this IEnumerable<MocktailDTO> mocktailDtosToConvert)
    {
        if (mocktailDtosToConvert == null)
            throw new ArgumentNullException(nameof(mocktailDtosToConvert), "MocktailDTOs collection cannot be null.");

        return mocktailDtosToConvert.Select(mocktailDTO => mocktailDTO.ToModel());
    }
}
