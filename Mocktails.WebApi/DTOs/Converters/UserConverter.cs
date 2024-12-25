using Mocktails.DAL.Model;

namespace Mocktails.WebApi.DTOs.Converters;

public static class UserConverter
{
    public static UserDTO ToDTO(this User userToConvert)
    {
        if (userToConvert == null)
            throw new ArgumentNullException(nameof(userToConvert), "User object cannot be null.");

        var userDTO = new UserDTO();
        userToConvert.CopyPropertiesTo(userDTO);
        return userDTO;
    }

    public static User ToModel(this UserDTO userDTOToConvert)
    {
        if (userDTOToConvert == null)
            throw new ArgumentNullException(nameof(userDTOToConvert), "UserDTO object cannot be null.");

        var user = new User();
        userDTOToConvert.CopyPropertiesTo(user);
        return user;
    }

    public static IEnumerable<UserDTO> ToDtos(this IEnumerable<User> usersToConvert)
    {
        if (usersToConvert == null)
            throw new ArgumentNullException(nameof(usersToConvert), "Users collection cannot be null.");

        return usersToConvert.Select(user => user.ToDTO());
    }

    public static IEnumerable<User> ToModels(this IEnumerable<UserDTO> userDtosToConvert)
    {
        if (userDtosToConvert == null)
            throw new ArgumentNullException(nameof(userDtosToConvert), "UserDTOs collection cannot be null.");

        return userDtosToConvert.Select(userDTO => userDTO.ToModel());
    }
}
