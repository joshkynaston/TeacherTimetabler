using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TeacherTimetabler.Api.DTOs;

[ExcludeFromCodeCoverage]
public record ClassDTO(int Id, string Name, string? Subject)
{
    [Required]
    public required int Id = Id;

    [Required]
    [MaxLength(25)]
    public required string Name = Name;

    [MaxLength(25)]
    public string? Subject = Subject;
}
