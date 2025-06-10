using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Interfaces.Services;

namespace ProjetAtrst.Controllers
{

    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly IUserAccessService _userAccessService;

        public DashboardController(IDashboardService dashboardService, IUserAccessService userAccessService)
        {
            _dashboardService = dashboardService;
            _userAccessService = userAccessService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userAccessService.GetUserId();
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var model = await _dashboardService.GetDashboardDataAsync(userId);
            var accessStatus = await _userAccessService.GetAccessStatusAsync(userId);

            ViewBag.AccessStatus = accessStatus; // يمكن استخدامه في View

            return View(model);
        }
    }


}
