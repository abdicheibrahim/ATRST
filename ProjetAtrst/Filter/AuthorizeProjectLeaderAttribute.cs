using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetAtrst.Interfaces.Services;
using System.Security.Claims;
public class AuthorizeProjectLeaderAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // محاولة الحصول على projectId من ActionArguments أولاً
        int projectId;

        if (context.ActionArguments.TryGetValue("projectId", out var projectIdObj) && projectIdObj != null)
        {
            projectId = (int)projectIdObj;
        }
        else
        {
            // إذا لم يكن موجوداً في ActionArguments، نحاول من Route أو Query
            if (!context.RouteData.Values.TryGetValue("projectId", out var routeProjectId) ||
                !int.TryParse(routeProjectId?.ToString(), out projectId))
            {
                var query = context.HttpContext.Request.Query;
                if (!query.TryGetValue("projectId", out var queryProjectId) ||
                    !int.TryParse(queryProjectId, out projectId))
                {
                    context.Result = new BadRequestResult(); // 400
                    return;
                }
            }
        }

        // الحصول على معرف المستخدم
        var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            context.Result = new ForbidResult(); // 403
            return;
        }

        // الحصول على خدمة المشروع
        var projectService = context.HttpContext.RequestServices.GetService<IProjectService>();

        if (projectService == null)
        {
            context.Result = new StatusCodeResult(500); // خطأ داخلي
            return;
        }

        // التحقق من أن المستخدم هو القائد
        var isLeader = await projectService.IsUserLeaderAsync(userId, projectId);

        if (!isLeader)
        {
            context.Result = new ForbidResult(); // 403
            return;
        }

        // السماح بالوصول
        await next();
    }
}