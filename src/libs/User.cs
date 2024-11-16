namespace SOLID_Principles.Domain;

/// <summary>
/// Domain model describing a user.
/// </summary>
/// <param name="name">The name of the user.</param>
/// <param name="email">The email belonging to the user.</param>
public class User(string name, string email)
{
    /// <summary>
    /// Gets or sets the id for the user
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>

    public string Name { get; set; } = name;

    /// <summary>
    /// Gets or sets the email of the user
    /// </summary>
    public string Email { get; set; } = email;
}
