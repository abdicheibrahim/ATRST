using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Services.Interfaces;
using ProjetAtrst.ViewModel.Researcher;
using System.Security.Claims;

namespace ProjetAtrst.Controllers
{
    public class ResearcherController : Controller
    {
        private readonly IResearcherService _researcherService;

        public ResearcherController(IResearcherService researcherService)
        {
            _researcherService = researcherService;
        }

        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = await _researcherService.GetProfileAsync(userId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(ResearcherProfileViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _researcherService.UpdateProfileAsync(userId, model);

            return RedirectToAction("Profile");
        }
    }
}
