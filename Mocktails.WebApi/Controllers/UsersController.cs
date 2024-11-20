using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Users.DTOs;
using Mocktails.DAL.DaoClasses;

namespace Mocktails.WebApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserDAO _userDAO;
    
    public UsersController(IUserDAO userDAO)
    {
        _userDAO = userDAO;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUserByIdAsync(int id)
    {
        var user = await _userDAO.GetUserByIdAsync(id);
        if(user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
}

