using SOLID_Principles.Domain;

namespace SOLID_Principles.Services.DTOs.Expenses;

public record PostExpenseDTO
{
    public int UserId { get; set; }
    public decimal Amount { get; set; } = 0;
    public Category Category { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string Description { get; set; } = string.Empty;
}
