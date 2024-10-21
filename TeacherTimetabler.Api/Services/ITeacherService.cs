using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Services;

public interface ITeacherService
{
  string? GetCurrentUserId();
  Task<Teacher?> GetCurrentUserAsync();
}
