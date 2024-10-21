using System.ComponentModel.DataAnnotations;

namespace TeacherTimetabler.Api.DTOs;

public record GetClassDTO([Required] int Id, [Required] [MaxLength(25)] string Name, [MaxLength(25)] string? Subject);

public record PostClassDTO([Required] string Name, [MaxLength(50)] string? Subject);
