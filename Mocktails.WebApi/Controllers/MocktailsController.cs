using Microsoft.AspNetCore.Mvc;
using Mocktails.ApiClient.Mocktails.DTOs;
using Mocktails.DAL.DaoClasses;

namespace Mocktails.WebApi.Controllers;

/// <summary>
/// This class provides basic CRUD functionality for BlogPosts in the system.
/// The controller receives a blogposts repository in its constructor, thereby lowering the coupling
/// and enabling the class responsible for creating the controller to provide any implementation of IBlogPostRepository
/// for testing purposes or for using a specific persistence mechanism (database/file/service/etc.)
/// </summary>
/// 

[Route("api/[controller]")]
[ApiController]
public class MocktailsController : ControllerBase
{

    #region Repository and constructor
    //The repository the controller should use for persistence
    IMocktailDAO _mocktailRepository;

    public MocktailsController(IMocktailDAO mocktailRepository) => _mocktailRepository = mocktailRepository;

    #endregion

    #region Default CRUD actions

    //public async Task<ActionResult<IEnumerable<MocktailDTO>>> GET([FromQuery] int? )
    //{
    //    return View();
    //}

    #endregion
}
