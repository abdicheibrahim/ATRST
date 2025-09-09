using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Services;
using ProjetAtrst.ViewModels.Account;
using ProjetAtrst.ViewModels.Researcher;
using System.Security.Claims;

namespace ProjetAtrst.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(ProfileCompletionFilter))]
    public class ResearcherController : Controller
    {
       
        private readonly IResearcherService _researcherService;

        public ResearcherController(IResearcherService researcherService)
        {

            _researcherService = researcherService;
            
        }
        // GET: /Researcher/EditProfile
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login");

            var model = await _researcherService.GetEditProfileResearcherViewModelAsync(userId);
            if (model == null)
                return NotFound(); 

            return View(model);
        }

        // POST: /Researcher/EditProfile
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditResearcherProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectToAction("Login");

            await _researcherService.EditProfileResearcherViewModelAsync(userId, model);

            return RedirectToAction("EditProfile");
        }
        [HttpGet]
        public async Task<IActionResult> Details(string Id)
        {
            var ResearcherDetails = await _researcherService.GetResearcherDetailsAsync(Id);
            if (ResearcherDetails == null)
                return NotFound();

            return PartialView("_ResearcherDetailsModal", ResearcherDetails);
        }
    }
}
