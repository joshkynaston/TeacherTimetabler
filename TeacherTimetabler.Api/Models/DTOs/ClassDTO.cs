using System.ComponentModel.DataAnnotations;

namespace TeacherTimetabler.Api.Models.DTOs;

public record ClassDTO
{
    public int Id { get; init; }

    [Required]
    public required string Name { get; init; }

    public string? Subject { get; init; }
}
