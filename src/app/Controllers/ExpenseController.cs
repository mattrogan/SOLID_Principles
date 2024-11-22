using Microsoft.AspNetCore.Mvc;
using SOLID_Principles.Services.DTOs.Expenses;
using SOLID_Principles.Services.ExpenseService;
using System.Net;

namespace SOLID_Principles.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _service;
    private readonly ILogger<ExpenseController> _logger;

    public ExpenseController(IExpenseService service, ILogger<ExpenseController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken token = default)
    {
        _logger.LogInformation("Fetching all expenses");

        var expenses = await _service.GetExpenses(exp => true, token);

        _logger.LogInformation("Found expenses - returning...");

        return Ok(expenses);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSingle(int id, CancellationToken token = default)
    {
        _logger.LogInformation("Attempting to fetch expense with id {Id}", id);

        var expense = await _service.GetExpense(id, token);

        return expense is null ? NotFound() : Ok(expense);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostExpenseDTO model, CancellationToken token = default)
    {
        if (model is null || !ModelState.IsValid)
        {
            _logger.LogInformation("The model was not valid.");
            return ValidationProblem(ModelState);
        }

        var expense = await _service.AddExpense(model, token);

        return expense is null ? StatusCode((int)HttpStatusCode.ServiceUnavailable) : Created(nameof(GetSingle), expense);
    }
}
