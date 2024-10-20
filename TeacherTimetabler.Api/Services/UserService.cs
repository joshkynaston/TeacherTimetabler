using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TeacherTimetabler.Api.Interfaces;
using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Services;

public class UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
    : IUserService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string? GetCurrentUserId()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public async Task<User?> GetCurrentUserAsync()
    {
        var userId = GetCurrentUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return null;
        }
        return await _userManager.FindByIdAsync(userId);
    }
}
