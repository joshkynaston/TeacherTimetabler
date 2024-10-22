using System.ComponentModel.DataAnnotations;

namespace TeacherTimetabler.Api.DTOs;

public record GetClassDto(
  [Required] int ClassId,
  [Required] [MaxLength(25)] string Name,
  [MaxLength(25)] string? Subject
);

public record PostClassDto([Required] string Name, [MaxLength(50)] string? Subject);
