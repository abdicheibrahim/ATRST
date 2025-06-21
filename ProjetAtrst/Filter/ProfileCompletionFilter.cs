using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetAtrst.Interfaces.Services;

public class ProfileCompletionFilter : IAsyncActionFilter
{
    private readonly IResearcherService _researcherService;

    public ProfileCompletionFilter(IResearcherService researcherService)
    {
        _researcherService = researcherService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;

        if (user.Identity != null && user.Identity.IsAuthenticated)
        {
            var userId = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var isProfileComplete = await _researcherService.IsProfileCompleteAsync(userId);

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
