using System.Net;
using Microsoft.AspNetCore.Mvc;
using SOLID_Principles.Services.DTOs.Users;
using SOLID_Principles.Services.UserService;

namespace SOLID_Principles.Controllers;

/// <summary>
/// Initialise an instance of the controller.
/// </summary>
/// <param name="service">DI User Service</param>
/// <param name="logger">DI Logger</param>
[ApiController]
public class UserController(IUserService service, ILogger<UserController> logger) : ControllerBase
{
    private readonly IUserService _service = service;
    private readonly ILogger<UserController> _logger = logger;

    /// <summary>
    /// Method to fetch all users
    /// </summary>
    /// <param name="token">Async Cancellation Token</param>
    /// <returns>An <see cref="IActionResult"/> containing the response to the request</returns>
    [HttpGet("api/User")]
    public async Task<IActionResult> GetUsersAsync(CancellationToken token = default)
    {
        _logger.LogInformation("Fetching users from database");

        var users = await _service.GetUsers(token);

        return Ok(users);
    }

    /// <summary>
    /// Method to fetch a single users
    /// </summary>
    /// <param name="id">The unique identifier for the user to find</param>
    /// <param name="token">Async Cancellation Token</param>
    /// <returns>An <see cref="IActionResult"/> containing the response to the request</returns>
    [HttpGet("api/User({id:int})")]
    public async Task<IActionResult> GetUserAsync(int id, CancellationToken token = default)
    {
        _logger.LogInformation("Fetching user with id {UserId} from database", id);

        var user = await _service.GetUser(id, token);

        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost("api/User")]
    public async Task<IActionResult> PostUserAsync(PostUserDTO model, CancellationToken token = default)
    {
        _logger.LogInformation("Attempting to create new user.");

        if (model is null || !ModelState.IsValid)
        {
            _logger.LogInformation("Model was not valid.");
            return ValidationProblem(ModelState);
        }

        var user = await _service.CreateUserAsync(model, token);

        if (user is null)
        {
            _logger.LogError("There was an error whilst creating the user.");
            return StatusCode((int)HttpStatusCode.ServiceUnavailable);
        }

        return Created(nameof(GetUserAsync), user);
    }
}
