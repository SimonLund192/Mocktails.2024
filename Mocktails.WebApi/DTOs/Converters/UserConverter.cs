using Mocktails.DAL.Model;

namespace Mocktails.WebApi.DTOs.Converters;

public static class UserConverter
{
    public static UserDTO ToDTO(this User userToConvert)
    {
        ArgumentNullException.ThrowIfNull(userToConvert);

        var userDTO = new UserDTO();
        userToConvert.CopyPropertiesTo(userDTO);
        return userDTO;
    }

    public static User ToModel(this UserDTO userDTOToConvert)
    {
        ArgumentNullException.ThrowIfNull(userDTOToConvert);

        var user = new User();
        userDTOToConvert.CopyPropertiesTo(user);
        return user;
    }

    public static IEnumerable<UserDTO> ToDtos(this IEnumerable<User> usersToConvert)
    {
        ArgumentNullException.ThrowIfNull(usersToConvert);

        return usersToConvert.Select(user => user.ToDTO());
    }

    public static IEnumerable<User> ToModels(this IEnumerable<UserDTO> userDtosToConvert)
    {
        ArgumentNullException.ThrowIfNull(userDtosToConvert);

        return userDtosToConvert.Select(userDTO => userDTO.ToModel());
    }
}
