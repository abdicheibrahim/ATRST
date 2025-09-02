using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels.Associate;
using ProjetAtrst.ViewModels.Partner;
using System.Security.Claims;

namespace ProjetAtrst.Controllers
{
    public class AssociateController : Controller
    {
        private readonly IAssociateService _associateService;
        public AssociateController(IAssociateService associateService)
        {
            _associateService = associateService;
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login");

            var model = await _associateService.GetEditProfileAssociateAsync(userId);
            if (model == null)
                return NotFound(); // or Redirect

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditAssociateProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login");
            await _associateService.EditProfileAssociateAsync(userId, model);
            return RedirectToAction("EditProfile");
        }
    }
}
