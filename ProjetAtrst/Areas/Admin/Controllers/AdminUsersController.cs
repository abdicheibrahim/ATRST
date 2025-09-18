using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ProjetAtrst.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class AdminUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminUsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // عرض قائمة الإدمنز
        public async Task<IActionResult> Index()
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            return View(admins);
        }

        // GET: Add Admin
        public IActionResult Create()
        {
            return View();
        }

        // POST: Add Admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "الرجاء إدخال البريد وكلمة السر");
                return View();
            }

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                RoleType = RoleType.Admin,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View();
        }
    }
    
}
