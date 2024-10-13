using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TeacherTimetabler.Api.Models;

[ExcludeFromCodeCoverage]
public class RegisterDTO
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }
}
