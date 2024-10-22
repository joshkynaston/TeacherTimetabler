using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Services;

public class TeacherService(UserManager<Teacher> userManager, IHttpContextAccessor httpContextAccessor)
  : ITeacherService
{
  public string? GetCurrentUserId()
  {
    return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
  }

  public async Task<Teacher?> GetCurrentUserAsync()
  {
    var userId = GetCurrentUserId();
    if (string.IsNullOrEmpty(userId))
      return null;

    return await userManager.FindByIdAsync(userId);
  }
}
