using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Models;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels.Identity;
using ProjetAtrst.ViewModels.Researcher;

namespace ProjetAtrst.Controllers
{
    public class AccountController : Controller
    {
        private readonly IResearcherService _researcherService;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(IResearcherService researcherService, IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _researcherService = researcherService;
            _userService = userService;
            _userManager = userManager;
        }

        // GET: /Account/Register
        public IActionResult Register() => View();

        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _researcherService.RegisterNewResearcherAsync(model);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // GET: /Account/Login
        public IActionResult Login() => View();

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await _researcherService.LoginAsync(model);
            if (success)
                return RedirectToAction("Index", "Dashboard");

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        // GET: /Account/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var dashboard = await _researcherService.GetDashboardAsync(User);
            if (dashboard == null)
                return NotFound();

            return View(dashboard);
        }

       
        // GET: /Account/Logout
        public async Task<IActionResult> Logout()
        {
            await _researcherService.LogoutAsync();
            return RedirectToAction("Login", "Account");

        }

        // GET: /Account/CompleteProfile
        [HttpGet]
        public async Task<IActionResult> CompleteProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login");

            var model = await _userService.GetCompleteProfileViewModelAsync(user.Id);
            if (model == null)
                return NotFound(); // or Redirect

            return View(model);
        }

        // POST: /Account/CompleteProfile
        [HttpPost]
        public async Task<IActionResult> CompleteProfile(CompleteProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login");

            await _userService.CompleteUserProfileAsync(user.Id, model);

            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/ChooseRole
        [HttpGet]
        public IActionResult ChooseRole()
        {
            return View();
        }

        // POST: /Account/JoinProject
        [HttpPost]
        public IActionResult JoinProject()
        {
            // Redirect to available projects or search page
            return RedirectToAction("AvailableProjects", "Team");
        }

        // POST: /Account/BecomeLeader
        [HttpPost]
        public IActionResult BecomeLeader()
        {
            // Redirect to create project page
            return RedirectToAction("CreateProject", "Project");
        }
    }
}




