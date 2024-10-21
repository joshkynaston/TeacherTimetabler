using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Services;

public class TeacherService(UserManager<Teacher> userManager, IHttpContextAccessor httpContextAccessor)
  : ITeacherService
{
  private readonly UserManager<Teacher> _userManager = userManager;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  public string? GetCurrentUserId()
  {
    return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
  }

  public async Task<Teacher?> GetCurrentUserAsync()
  {
    var userId = GetCurrentUserId();
    if (string.IsNullOrEmpty(userId))
    {
      return null;
    }
    return await _userManager.FindByIdAsync(userId);
  }
}
