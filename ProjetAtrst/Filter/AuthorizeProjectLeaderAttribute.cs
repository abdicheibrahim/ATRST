using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetAtrst.Interfaces.Services;
using System.Security.Claims;

public class AuthorizeProjectLeaderAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Get projectId from Route
        if (!context.ActionArguments.TryGetValue("projectId", out var projectIdObj) || projectIdObj == null)
        {
            context.Result = new BadRequestResult();
            return;
        }

        var projectId = (int)projectIdObj;

        // Get current user ID
        var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            context.Result = new ForbidResult();
            return;
        }

        // Call service from DI Container
        var projectService = context.HttpContext.RequestServices
            .GetService(typeof(IProjectService)) as IProjectService;

        if (projectService == null)
        {
            context.Result = new StatusCodeResult(500); // Initialization error
            return;
        }

        // Check if user is leader
        var isLeader = await projectService.IsUserLeaderAsync(userId, projectId);

        if (!isLeader)
        {
            context.Result = new ForbidResult(); // Or redirect to permissions page
            return;
        }

        // Allow access
        await next();
    }
}
