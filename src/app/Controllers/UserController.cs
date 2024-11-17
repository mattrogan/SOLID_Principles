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
[Route("api/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    private readonly IUserService _service = service;

    /// <summary>
    /// Method to fetch all users
    /// </summary>
    /// <param name="token">Async Cancellation Token</param>
    /// <returns>An <see cref="IActionResult"/> containing the response to the request</returns>
    [HttpGet]
    public async Task<IActionResult> GetUsersAsync(CancellationToken token = default)
    {
        var users = await _service.GetUsers(token);

        return Ok(users);
    }

    /// <summary>
    /// Method to fetch a single users
    /// </summary>
    /// <param name="id">The unique identifier for the user to find</param>
    /// <param name="token">Async Cancellation Token</param>
    /// <returns>An <see cref="IActionResult"/> containing the response to the request</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserAsync(int id, CancellationToken token = default)
    {
        var user = await _service.GetUser(id, token);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> PostUserAsync(PostUserDTO model, CancellationToken token = default)
    {

        if (model is null || !ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var user = await _service.CreateUserAsync(model, token);

        if (user is null)
        {
            return StatusCode((int)HttpStatusCode.ServiceUnavailable);
        }

        return Created(nameof(GetUserAsync), user);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutUserAsync(int id, PostUserDTO model, CancellationToken token = default)
    {
        if (model is null || ModelState.IsValid == false)
        {
            return ValidationProblem(ModelState);
        }

        var user = await _service.GetUser(id, token);
        if (user is null)
        {
            return NotFound();
        }

        user = await _service.UpdateUserAsync(id, model, token);
        if (user is null)
        {
            return StatusCode((int)HttpStatusCode.ServiceUnavailable);
        }

        return Ok(user);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken token)
    {
        var user = await _service.GetUser(id, token);
        if (user == null)
        {
            return NotFound();
        }

        if (!await _service.DeleteUserAsync(id, token))
        {
            return StatusCode((int)HttpStatusCode.ServiceUnavailable);
        }

        return NoContent();
    }
}
