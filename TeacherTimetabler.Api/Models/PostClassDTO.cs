using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TeacherTimetabler.Api.Models;

[ExcludeFromCodeCoverage]
public record PostClassDTO
{
    [Required]
    public required string Name { get; init; }

    public string? Subject { get; init; }
}
