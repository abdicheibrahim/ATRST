using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Helpers;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.Account;
using System.Linq;
using System.Security.Claims;
using ProjetAtrst.Helpers;

namespace ProjetAtrst.Controllers
{
    public class AccountController : Controller
    {
        private readonly IResearcherService _researcherService;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly StaticDataLoader _staticDataLoader ;

        public AccountController(IResearcherService researcherService, IUserService userService, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment, StaticDataLoader staticDataLoader)
        {
            _researcherService = researcherService;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _staticDataLoader = staticDataLoader;
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
                return RedirectToAction("Index", "Dashboard");

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

            var success = await _userService.LoginAsync(model);
            if (success)
                return RedirectToAction("Index", "Dashboard");

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }



        // GET: /Account/Logout
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return RedirectToAction("Login", "Account");

        }



        // GET: /Account/CompleteProfile
        [HttpGet]
        public async Task<IActionResult> CompleteProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login");

            var model = await _userService.GetCompleteProfileViewModelAsync(userId);
            if (model == null)
                return NotFound();

            return View(model);
        }

        // POST: /Account/CompleteProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteProfile(CompleteProfileViewModel model)
        {
          

            if (!ModelState.IsValid)
            {
                TempData["ShowErrorModal"] = true;
                TempData["ErrorTitle"] = "Oops !";
                TempData["ErrorMessage"] = "Une erreur est survenue lors de l'enregistrement.";
                return View(model); 
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return RedirectToAction("Login");

            await _userService.CompleteProfileAsync(userId, model);
            return RedirectToAction("Index", "Dashboard");
        }



        // GET: /Account/EditProfile
        [Authorize]
        [ServiceFilter(typeof(ProfileCompletionFilter))]
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login");

            var model = await _userService.GetEditProfileViewModelAsync(userId);
            if (model == null)
                return NotFound(); // or Redirect

            return View(model);
        }

        // POST: /Account/EditProfile
        [Authorize]
        [ServiceFilter(typeof(ProfileCompletionFilter))]
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login");

            await _userService.EditProfileAsync(userId, model);
            TempData["ShowSuccessModal"] = true;
            TempData["SuccessTitle"] = "Parfait !";
            TempData["SuccessMessage"] = "Les données ont été enregistrées avec succès.";
            return RedirectToAction("EditProfile");
        }



        // GET: /Account/EditAccount
        [ServiceFilter(typeof(ProfileCompletionFilter))]
        [HttpGet]
        public async Task<IActionResult> EditAccount()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var model = new EditAccountViewModel
            {
                Email = user.Email,
                ExistingProfilePicturePath = user.ProfilePicturePath
            };

            return View(model);
        }

        // POST: /Account/EditAccount
        [HttpPost]
        public async Task<IActionResult> EditAccount(EditAccountViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            if (!ModelState.IsValid)
            {
                //----Modal-------
                TempData["ShowErrorModal"] = true;
                TempData["ErrorTitle"] = "Oops !";
                TempData["ErrorMessage"] = "Une erreur est survenue lors de l'enregistrement.";
                //-----------------
                return View(model);
            }
            // تحديث الصورة فقط
            if (model.ProfilePicture != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(model.ProfilePicture.FileName).ToLowerInvariant();
                var maxSize = 2 * 1024 * 1024;

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("ProfilePicture", "Seules les images JPG, JPEG ou PNG sont autorisées.");
                    //----Modal-------
                    TempData["ShowErrorModal"] = true;
                    TempData["ErrorTitle"] = "Oops !";
                    TempData["ErrorMessage"] = "ProfilePicture, Seules les images JPG, JPEG ou PNG sont autorisées.";
                    //-----------------
                    return View(model);
                }

                if (model.ProfilePicture.Length > maxSize)
                {
                    ModelState.AddModelError("ProfilePicture", "L’image ne doit pas dépasser 2MB.");
                    //----Modal-------
                    TempData["ShowErrorModal"] = true;
                    TempData["ErrorTitle"] = "Oops !";
                    TempData["ErrorMessage"] = "ProfilePicture, L’image ne doit pas dépasser 2MB";
                    //-----------------
                    return View(model);
                }

                if (!string.IsNullOrEmpty(user.ProfilePicturePath))
                {
                    var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, user.ProfilePicturePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                var fileName = $"{Guid.NewGuid()}{extension}";
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "profile");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fullPath = Path.Combine(folderPath, fileName);
                using var stream = new FileStream(fullPath, FileMode.Create);
                await model.ProfilePicture.CopyToAsync(stream);

                user.ProfilePicturePath = "/uploads/profile/" + fileName;
            }

            // ✅ تحديث البريد فقط
            if (user.Email != model.Email)
            {
                var result = await _userManager.SetEmailAsync(user, model.Email);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Erreur lors de la mise à jour de l'email.");
                    //----Modal-------
                    TempData["ShowErrorModal"] = true;
                    TempData["ErrorTitle"] = "Oops !";
                    TempData["ErrorMessage"] = "Erreur lors de la mise à jour de l'email.";
                    //-----------------
                    return View(model);
                }
            }

            // ✅ تغيير كلمة السر فقط إذا كتب NewPassword
            if (!string.IsNullOrWhiteSpace(model.NewPassword))
            {
                if (string.IsNullOrWhiteSpace(model.CurrentPassword))
                {
                    ModelState.AddModelError("CurrentPassword", "Veuillez saisir le mot de passe actuel pour le modifier.");
                    return View(model);
                }

                var changePassResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!changePassResult.Succeeded)
                {
                    ModelState.AddModelError("", "Mot de passe actuel incorrect ou nouveau mot de passe invalide.");
                    return View(model);
                }
            }

            await _userManager.UpdateAsync(user);
            //TempData["Success"] = "Le compte a été mis à jour avec succès.";
            TempData["ShowSuccessModal"] = true;
            TempData["SuccessTitle"] = "Parfait !";
            TempData["SuccessMessage"] = "Le compte a été mis à jour avec succès.";
            return RedirectToAction("EditAccount");
        }

    }
}




