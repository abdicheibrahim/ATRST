using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetAtrst.Interfaces.Services;

public class ProfileCompletionFilter : IAsyncActionFilter
{
   
    private readonly IUserService _userService;

    public ProfileCompletionFilter(IResearcherService researcherService,IUserService userService)
    {
       
        _userService = userService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;

        if (user.Identity != null && user.Identity.IsAuthenticated)
        {
            var userId = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var isProfileComplete = await _userService.IsProfileCompleteAsync(userId);

                if (!isProfileComplete)
                {
                    context.Result = new RedirectToActionResult("CompleteProfile", "Account", null);
                    return;
                }
            }
        }

        await next();
    }
}
