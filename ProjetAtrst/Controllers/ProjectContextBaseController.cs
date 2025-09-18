using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetAtrst.Interfaces.Services;
using System.Security.Claims;

namespace ProjetAtrst.Controllers
{

    public class ProjectContextBaseController : Controller
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionName = context.RouteData.Values["action"]?.ToString();
            var projectId = HttpContext.Session.GetInt32("CurrentProjectId");

            // تعيين projectId في ViewBag إذا كان موجوداً
            if (projectId != null)
            {
                ViewBag.CurrentProjectId = projectId;
            }

            // حماية جميع النقاط ما عدا Enter
            if (actionName != "Enter")
            {
                if (projectId == null)
                {
                    context.Result = new BadRequestResult();
                    return;
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    context.Result = new ForbidResult();
                    return;
                }

               
                var projectService = HttpContext.RequestServices.GetService<IProjectService>();
                if (projectService != null)
                {
                    var hasAccess = await projectService.IsUserLeaderAsync(userId, projectId.Value);
                    if (!hasAccess)
                    {
                        context.Result = new ForbidResult();
                        return;
                    }
                }
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }

}
