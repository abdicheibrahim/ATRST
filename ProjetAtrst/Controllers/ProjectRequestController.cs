using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels.ProjectRequests;
using System.Security.Claims;
using ProjetAtrst.Enums;

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

        [HttpGet]
        public async Task<IActionResult> Send(int projectId, string receiverId, RequestType type)
        {
            var model = await _requestService.PrepareRequestModelAsync(projectId, receiverId, type);

            if (model == null)
                return NotFound("المشروع غير موجود.");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(ProjectRequestCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _requestService.SendRequestAsync(model, senderId);

            TempData["Success"] = model.Type == RequestType.Join ?
                "تم إرسال طلب الانضمام بنجاح." :
                "تم إرسال الدعوة بنجاح.";

            return RedirectToAction("SentJoinRequests", "ProjectRequest"); // أو SendInvitations
        }

        public async Task<IActionResult> Accept(int id)
        {
            await _requestService.AcceptRequestAsync(id);
            TempData["Success"] = "تم قبول الطلب.";
            return RedirectToAction("Incoming");
        }

        public async Task<IActionResult> Reject(int id)
        {
            await _requestService.RejectRequestAsync(id, RejectionType.JoinRequest);

            TempData["Warning"] = "تم رفض الطلب.";
            return RedirectToAction("Incoming");
        }

        public async Task<IActionResult> Details(int id)
        {
            var request = await _requestService.GetByIdWithRelationsAsync(id);
            if (request == null) return NotFound();

            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> SentJoinRequests()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var requests = await _requestService.GetSentJoinRequestsAsync(userId);
            return View(requests);
        }

        [HttpGet]
        public async Task<IActionResult> SentInvitations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var invitations = await _requestService.GetMyInvitationsAsync(userId);
            return View(invitations);
        }

        //Invitations to join
    }

}
