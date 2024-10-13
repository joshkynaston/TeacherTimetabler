using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TeacherTimetabler.Api.Data;

namespace TeacherTimetabler.Api.Security;

public class ResourceOwnershipHandler(AppDbContext dbCtx, ILogger<ResourceOwnershipHandler> logger)
    : AuthorizationHandler<ResourceOwnershipRequirement>
{
    private readonly AppDbContext _dbCtx = dbCtx;
    private readonly ILogger<ResourceOwnershipHandler> _logger = logger;

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ResourceOwnershipRequirement requirement
    )
    {
        // Extract user ID from the current context (JWT or cookie)
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            _logger.LogError("User ID is null, failing authorization.");
            context.Fail();
            return;
        }

        // Extract RouteData from HttpContext to get the resource (class) ID
        if (context.Resource is HttpContext httpContext)
        {
            var routeData = httpContext.GetRouteData();

            var entityId = int.TryParse(routeData?.Values["id"]?.ToString(), out var id)
                ? id
                : (int?)null;

            if (entityId == null)
            {
                _logger.LogError("Entity ID is null, failing authorization.");
                context.Fail();
                return;
            }

            // Check if the class exists and is owned by the current user
            var resource = await _dbCtx.Classes.FirstOrDefaultAsync(c => c.Id == entityId);
            if (resource == null || resource.UserEntityId != userId)
            {
                _logger.LogError(
                    $"Class not found or not owned by user {userId}, failing authorization."
                );
                context.Fail();
                return;
            }

            _logger.LogInformation($"User {userId} is authorized to access class {entityId}.");
            context.Succeed(requirement);
        }
        else
        {
            _logger.LogError("Resource is not of type HttpContext, failing authorization.");
            context.Fail();
        }
    }
}
