using System;
using System.ComponentModel.DataAnnotations;

namespace SOLID_Principles.Services.DTOs.Users;

public record PostUserDTO
{
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
