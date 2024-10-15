using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
namespace TeacherTimetabler.Api.DTOs;


[ExcludeFromCodeCoverage]
public class LoginDTO
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}
