using Microsoft.AspNetCore.Mvc;

using Mocktails.DAL.DaoClasses;
using Mocktails.DAL.Model;
using Microsoft.AspNetCore.Identity;
using Mocktails.WebApi.DTOs.Converters;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.Eventing.Reader;
using Mocktails.WebApi.DTOs;

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
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAsync([FromQuery] string email)
        {
            IEnumerable<User> users = null;
            if (!string.IsNullOrEmpty(email)) { users = new List<User>() { await _userDAO.GetUserByEmailAsync(email) }; }
            else { users = await _userDAO.GetAllUsersAsync(); }
            return Ok(users.ToDtos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetAsync(int id)
        {
            var user = await _userDAO.GetUserByIdAsync(id);
            if (user == null) { return NotFound(); }
            else { return Ok(user.ToDTO()); }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] UserDTO newUserDTO)
        {
            await _userDAO.CreateUserAsync(newUserDTO.ToModel(), newUserDTO.PasswordHash);
            return Created();
        }


        #endregion


    }
}
