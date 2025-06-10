using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Models;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels;

namespace ProjetAtrst.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        //private readonly IUserAccessService _accessService;
        //private readonly UserManager<ApplicationUser> _userManager;

        //public SidebarViewComponent(IUserAccessService accessService, UserManager<ApplicationUser> userManager)
        //{
        //    _accessService = accessService;
        //    _userManager = userManager;
        //}

        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    var user = await _userManager.GetUserAsync(HttpContext.User);
        //    if (user == null)
        //        return View(new SidebarViewModel());

        //    var (isCompleted, isApproved, ProjectMember, ProjectLeader) = await _accessService.GetAccessStatusAsync(user.Id);

        //    return View(new SidebarViewModel
        //    {
        //        IsCompleted = isCompleted,
        //        IsApproved = isApproved,
        //        ProjectMember = ProjectMember,
        //        ProjectLeader = ProjectLeader
        //    });
        //}
    }
}
