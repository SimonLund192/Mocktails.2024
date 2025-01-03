using System.ComponentModel.DataAnnotations;

namespace Mocktails.ApiClient.Users.DTOs;

public class UserDTO
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
    //public string PasswordHash { get; set; }
    //public string Role { get; set; }
}
