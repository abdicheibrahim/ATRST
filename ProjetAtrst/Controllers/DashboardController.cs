using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.Dashboard;
using System.Security.Claims;

namespace ProjetAtrst.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(ProfileCompletionFilter))]

    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
       

        public DashboardController(IDashboardService dashboardService, UserManager<ApplicationUser> userManager)
        {
            _dashboardService = dashboardService;
           
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var leaderProjects = await _dashboardService.GetMyLeaderProjectsAsync(userId);
            var joinedProjects = await _dashboardService.GetMyJoinedProjectsAsync(userId);

            var viewModel = new DashboardViewModel
            {
                LeaderProjects = leaderProjects,
                JoinedProjects = joinedProjects
            };

            return View(viewModel);
        }
    }
}
