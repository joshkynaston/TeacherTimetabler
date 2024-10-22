using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TeacherTimetabler.Api.Data;

namespace TeacherTimetabler.Api.Security;

public class ResourceOwnershipHandler(AppDbContext dbCtx, ILogger<ResourceOwnershipHandler> logger)
  : AuthorizationHandler<ResourceOwnershipRequirement>
{
  protected override async Task HandleRequirementAsync(
    AuthorizationHandlerContext context,
    ResourceOwnershipRequirement requirement
  )
  {
    // Extract user ID from context
    var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (userId == null)
    {
      logger.LogError("User ID is null, failing authorization.");
      context.Fail();
      return;
    }

    // Extract RouteData from HttpContext to get the resource ID
    if (context.Resource is HttpContext httpContext)
    {
      var routeData = httpContext.GetRouteData();
      var entityId = int.TryParse(routeData?.Values["id"]?.ToString(), out var id) ? id : (int?)null;
      if (entityId == null)
      {
        logger.LogError("Entity ID is null, failing authorization.");
        context.Fail();
        return;
      }

      // Check if the entity exists and is owned by the current user
      var resource = await dbCtx.Classes.FirstOrDefaultAsync(c => c.Id == entityId);
      if (resource == null || resource.TeacherId != userId)
      {
        logger.LogError("Class not found or not owned by user {userId}, failing authorization.", userId);
        context.Fail();
        return;
      }

      logger.LogInformation("User {userId} is authorized to access class {entityId}.", userId, entityId);
      context.Succeed(requirement);
    }
    else
    {
      logger.LogError("Resource is not of type HttpContext, failing authorization.");
      context.Fail();
    }
  }
}
