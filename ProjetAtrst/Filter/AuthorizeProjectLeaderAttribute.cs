using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetAtrst.Interfaces.Services;
using System.Security.Claims;

public class AuthorizeProjectLeaderAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // جلب projectId من الـ Route
        if (!context.ActionArguments.TryGetValue("projectId", out var projectIdObj) || projectIdObj == null)
        {
            context.Result = new BadRequestResult();
            return;
        }

        var projectId = (int)projectIdObj;

        // جلب معرف المستخدم الحالي
        var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            context.Result = new ForbidResult();
            return;
        }

        // استدعاء الخدمة من DI Container
        var projectService = context.HttpContext.RequestServices
            .GetService(typeof(IProjectService)) as IProjectService;

        if (projectService == null)
        {
            context.Result = new StatusCodeResult(500); // خطأ في التهيئة
            return;
        }

        // التحقق هل هو القائد
        var isLeader = await projectService.IsUserLeaderAsync(userId, projectId);

        if (!isLeader)
        {
            context.Result = new ForbidResult(); // أو Redirect إلى صفحة صلاحيات
            return;
        }

        // السماح بالوصول
        await next();
    }
}
