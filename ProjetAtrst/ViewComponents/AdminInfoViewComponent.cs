using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels.ViewComponent;

namespace ProjetAtrst.ViewComponents
{
    public class AdminInfoViewComponent : ViewComponent
    
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotificationService _notificationService;

        public AdminInfoViewComponent(UserManager<ApplicationUser> userManager, INotificationService notificationService)
        {
            _userManager = userManager;
            _notificationService = notificationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //if (user == null)
            // return View("Default", null);

            var notifications = await _notificationService.GetUnreadNotificationsAsync(user.Id);

            var model = new UserInfoViewModel
            {
                FullName = user.FullName,
                ProfilePicturePath = user.ProfilePicturePath,
                Notifications = notifications
            };

            return View("Default", model);
        }
    }
}
