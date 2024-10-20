using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Interfaces;

public interface IUserService
{
    string? GetCurrentUserId();
    Task<User?> GetCurrentUserAsync();
}
