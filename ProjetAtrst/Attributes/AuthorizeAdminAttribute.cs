using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ProjetAtrst.Attributes
{
    public class AuthorizeAdminAttribute : AuthorizeAttribute, IAuthorizationFilter
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

            var isAdmin = db.Admins.Any(a => a.Id == userId);
            if (!isAdmin)
            {
                context.Result = new ForbidResult();
            }
        }
    }

}
