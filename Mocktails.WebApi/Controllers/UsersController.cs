using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Users.DTOs;
using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;
using Microsoft.AspNetCore.Identity;

namespace Mocktails.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserDAO _userDAO;
        private readonly PasswordHasher<User> _passwordHasher;  // Add the PasswordHasher

        public UsersController(IUserDAO userDAO)
        {
            _userDAO = userDAO;
            _passwordHasher = new PasswordHasher<User>(); // Initialize PasswordHasher
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                // Check if the email already exists in the database
                var existingUser = await _userDAO.GetUserByEmailAsync(userDTO.Email);
                if (existingUser != null)
                {
                    // Return a 400 Bad Request if the email already exists
                    return BadRequest("Email is already in use.");
                }

                // Convert UserDTO to User entity
                var user = new User
                {
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Email = userDTO.Email,
                    PasswordHash = userDTO.PasswordHash
                };

                // Hash the password before saving to the database
                user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);

                // Save user to the database
                var userId = await _userDAO.CreateUserAsync(user);

                // Use CreatedAtAction to return the newly created user and a link to the GetUserById action
                return CreatedAtAction(nameof(GetUserByIdAsync), new { id = userId }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating user: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserByIdAsync(int id)
        {
            var user = await _userDAO.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
