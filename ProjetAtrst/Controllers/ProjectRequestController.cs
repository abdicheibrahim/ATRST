using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Models;
using ProjetAtrst.Interfaces;
using ProjetAtrst.ViewModels.ProjectRequests;
using System.Security.Claims;

namespace ProjetAtrst.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(ProfileCompletionFilter))]
    public class ProjectRequestController : Controller
    {
        private readonly IProjectRequestService _requestService;

        public ProjectRequestController(IProjectRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<IActionResult> Incoming()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var (incoming, sent) = await _requestService.GetRequestsForDashboardAsync(userId);

            var viewModel = new RequestsDashboardViewModel
            {
                IncomingRequests = incoming,
                SentRequests = sent
            };

            return View(viewModel);
        }


        public async Task<IActionResult> Outgoing()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var requests = await _requestService.GetOutgoingRequestsAsync(userId);
            return View(requests);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(ProjectRequestCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _requestService.SendRequestAsync(model, senderId);
            TempData["Success"] = "تم إرسال الطلب بنجاح.";
            return RedirectToAction("Project", "AvailableProjects");
        }

        public async Task<IActionResult> Accept(int id)
        {
            await _requestService.AcceptRequestAsync(id);
            TempData["Success"] = "تم قبول الطلب.";
            return RedirectToAction("Incoming");
        }

        public async Task<IActionResult> Reject(int id)
        {
            await _requestService.RejectRequestAsync(id);
            TempData["Warning"] = "تم رفض الطلب.";
            return RedirectToAction("Incoming");
        }


    }

}
