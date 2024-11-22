namespace SOLID_Principles.Domain;

/// <summary>
/// Domain model describing an Expense.
/// </summary>
/// <param name="category">The category of the expense.</param>
/// <param name="amount">The amount of the expense.</param>
/// <param name="description">A description of the expense's costs.</param>
public class Expense
{
    public Expense()
    {
    }

    /// <summary>
    /// Gets or sets the ID for the expense.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the user the expense is associated with.
    /// </summary>
    public int UserId { get; init; }

    /// <summary>
    /// Gets or sets the amount for the expense.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the category of the expense.
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// Gets or sets the date of the expense.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets or sets a description of the expense.
    /// </summary>
    public string Description { get; set; }
}

/// <summary>
/// Enum describing the category for the expense.
/// </summary>
public enum Category
{
    /// <summary>
    /// Describes an expense related to food or drink.
    /// </summary>
    MEAL = 1,

    /// <summary>
    /// Describes an expense related to transport.
    /// </summary>
    TRANSPORT = 2,

    /// <summary>
    /// Describes an expense related to entertainment.
    /// </summary>
    ENTERTAINMENT = 3
}