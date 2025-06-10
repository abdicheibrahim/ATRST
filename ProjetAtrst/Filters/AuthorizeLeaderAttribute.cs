using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ProjetAtrst.Filters
{
    public class AuthorizeLeaderAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            var db = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var isLeader = db.ProjectLeaders.Any(l => l.Id == userId);
            if (!isLeader)
            {
                context.Result = new ForbidResult();
            }
        }
    }

}
