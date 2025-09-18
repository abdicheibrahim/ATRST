using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Services;
using ProjetAtrst.ViewModels.Partner;
using ProjetAtrst.ViewModels.Researcher;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjetAtrst.Controllers
{
    public class PartnerController : Controller
    {
        private readonly IPartnerService _partnerService;
        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login");

            var model = await _partnerService.GetEditProfilePartnerAsync(userId);
            if (model == null)
                return NotFound(); // or Redirect

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditPartnerProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login");
            await _partnerService.EditProfilePartnerAsync(userId, model);
            return RedirectToAction("EditProfile");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string Id)
        {
            var partnerDetails = await _partnerService.GetPartnerDetailsAsync(Id);
            if (partnerDetails == null)
                return NotFound();

            return PartialView("_PartnerDetailsModal", partnerDetails);
        }

    }
}
