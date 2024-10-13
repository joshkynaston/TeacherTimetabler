using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TeacherTimetabler.Api.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected bool TryGetUserId(out string userId)
    {
        userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        if (string.IsNullOrEmpty(userId))
        {
            return false;
        }
        return true;
    }
}
