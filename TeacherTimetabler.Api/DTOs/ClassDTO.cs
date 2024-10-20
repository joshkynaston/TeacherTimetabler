using System.ComponentModel.DataAnnotations;

namespace TeacherTimetabler.Api.DTOs;

public record ClassDTO(
    [Required] int Id,
    [Required] [MaxLength(25)] string Name,
    [MaxLength(25)] string? Subject
);
