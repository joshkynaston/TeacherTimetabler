using System.ComponentModel.DataAnnotations;

namespace TeacherTimetabler.Api.DTOs;

public record PostClassDto( //
  [Required] string Name,
  [MaxLength(50)] string? Subject
);
