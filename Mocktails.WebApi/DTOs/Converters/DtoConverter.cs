namespace Mocktails.WebApi.DTOs.Converters;

/// <summary>
/// Tool class for storing extension methods for converting DTOs to Model objects and back
/// </summary>
public static class DtoConverter
{
    #region Mocktail conversion methods
    public static MocktailDTO ToDtos(this MocktailDTO mocktailToConvert)
    {
        var mocktailDTO = new MocktailDTO();
        mocktailToConvert.CopyPropertiesTo(mocktailDTO);
        return mocktailDTO;
    }

    //public static Mocktail FromDTO(this MocktailDTO mocktailDTOToConvert)
    //{
    //    var mocktail = new Mocktail();
    //    mocktailToDTOConvert.CopyPropertiesTo(mocktail);
    //    return mocktail;
    //}

    #endregion
}
