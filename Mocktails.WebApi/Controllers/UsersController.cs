using Microsoft.AspNetCore.Mvc;

using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;
using Microsoft.AspNetCore.Identity;
using Mocktails.WebApi.DTOs.Converters;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.Eventing.Reader;
using Mocktails.WebApi.DTOs;
using Mocktails.DAL.Authentication;

namespace Mocktails.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserDAO _userDAO;


        public UsersController(IUserDAO userDAO)
        {
            _userDAO = userDAO;

        }

        #region Default CRUD actions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetByEmailAsync([FromQuery] string email)
        {
            IEnumerable<User> users = null;
            if (!string.IsNullOrEmpty(email)) { users = new List<User>() { await _userDAO.GetUserByEmailAsync(email) }; }
            else { users = await _userDAO.GetAllUsersAsync(); }
            return Ok(users.ToDtos());
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetUsersBySearch([FromQuery] string partOfName)
        {
            var users = await _userDAO.GetUserByPartOfNameAsync(partOfName);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetAsync(int id)
        {
            var user = await _userDAO.GetUserByIdAsync(id);
            if (user == null) { return NotFound(); }
            else { return Ok(user.ToDTO()); }
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateUser([FromBody] UserDTO userDTO)
        {
            var user = UserConverter.ToModel(userDTO);
            var userId = await _userDAO.CreateUserAsync(user, user.PasswordHash);

            return Created();


            //await _userDAO.CreateUserAsync(newUserDTO.ToModel(), newUserDTO.PasswordHash);
            //return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                ModelState.AddModelError(nameof(id), "Id's must match");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = UserConverter.ToModel(userDTO);
            user.Id = id;

            var success = await _userDAO.UpdateUserAsync(user);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _userDAO.DeleteUserAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userDAO.GetUserByEmailAsync(loginDTO.Email);

            if (user == null || !VerifyPassword(loginDTO.Password, user.PasswordHash))
            {
                return Unauthorized(new { Message = "Invalid email or password" });
            }

            // Create a session or token if required
            return Ok(new { Message = "Login successful", UserId = user.Id });
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            return BCryptTool.ValidatePassword(password, passwordHash);
        }


        #endregion


    }
}
