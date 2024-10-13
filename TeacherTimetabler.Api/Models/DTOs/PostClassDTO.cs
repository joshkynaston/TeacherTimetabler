using System.ComponentModel.DataAnnotations;

namespace TeacherTimetabler.Api.Models.DTOs;

public record PostClassDTO
{
    [Required]
    public required string Name { get; init; }

    public string? Subject { get; init; }
}
