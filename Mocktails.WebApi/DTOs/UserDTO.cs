namespace Mocktails.WebApi.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    //public string Password { get; set; }
    public string PasswordHash { get; set; }
    //public string NewPassword { get; set; }
    //public string Role { get; set; }
}
