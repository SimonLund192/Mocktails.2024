using Mocktails.ApiClient.Users.DTOs;
using Mocktails.DAL.Model;

namespace Mocktails.WebApi.DTOs.Converters;

public static class UserConverter
{
    public static UserDTO ToDTO(UserDTO user)
    {
        return new UserDTO
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            Role = user.Role

        };
    }

    public static User ToModel(UserDTO userDTO)
    {
        return new User
        {
            Id = userDTO.Id,
            FirstName = userDTO.FirstName,
            LastName = userDTO.LastName,
            Email = userDTO.Email,
            PasswordHash = userDTO.PasswordHash,
            //Role = userDTO.Role

        };
    }
}
