using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjetAtrst.Controllers
{

    public class ProjectContextBaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var projectId = HttpContext.Session.GetInt32("CurrentProjectId");

            if (projectId != null)
                ViewBag.CurrentProjectId = projectId;

            base.OnActionExecuting(context);
        }
    }

}
