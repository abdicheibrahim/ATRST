using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Models;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels.Identity;
using ProjetAtrst.ViewModels.Researcher;
using ProjetAtrst.Services;

namespace ProjetAtrst.Controllers
{
    [Authorize]
    public class ResearcherController : Controller
    {
        private readonly IResearcherService _researcherService;
        private readonly IUserAccessService _userAccessService;
        private readonly IProjectService _projectService;
        private readonly IInvitationRequestService _invitationRequestService;

        public ResearcherController( IResearcherService researcherService,
        IUserAccessService userAccessService,
        IProjectService projectService,
        IInvitationRequestService invitationRequestService)
        {
            _researcherService = researcherService;
            _userAccessService = userAccessService;
            _projectService = projectService;
            _invitationRequestService = invitationRequestService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _userAccessService.GetUserId();
            if (userId == null) return Unauthorized();

            var leader = await _projectService.GetLeaderByResearcherIdAsync(userId);
            if (leader == null || leader.CreatedProject == null)
                return RedirectToAction("AccessDenied", "Account");

            var researchers = await _invitationRequestService.GetAllEligibleForInvitationAsync(leader.CreatedProject.Id);
            ViewBag.ProjectId = leader.CreatedProject.Id;
            return View(researchers);
        }
        // GET: /Researcher/Register
        public IActionResult Register() => View();

        // POST: /Researcher/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _researcherService.RegisterNewResearcherAsync(model);
            if (result.Succeeded)
                return RedirectToAction("Dashboard", "Researcher");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // GET: /Researcher/Login
        public IActionResult Login() => View();

        // POST: /Researcher/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await _researcherService.LoginAsync(model);
            if (success)
                return RedirectToAction("Dashboard", "Researcher");

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        // GET: /Researcher/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var dashboard = await _researcherService.GetDashboardAsync(User);
            if (dashboard == null)
                return NotFound();

            return View(dashboard);
        }


    }
}