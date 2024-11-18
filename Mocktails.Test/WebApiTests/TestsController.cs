using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mocktails.WebApi.Data;

namespace Mocktails.Test.WebApiTests;
[Route("api/test")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly MocktailsDbContext _dbContext;

    public TestController(MocktailsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult TestDatabaseConnection()
    {
        try
        {
            var result = _dbContext.Mocktails.Take(1).ToList();  // Fetch one record as a test
            return Ok("Database connected successfully. Mocktail count: " + result.Count);
        }
        catch (Exception ex)
        {
            return BadRequest("Database connection failed: " + ex.Message);
        }
    }
}
