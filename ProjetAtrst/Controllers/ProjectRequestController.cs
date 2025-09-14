using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Enums;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Models;
using ProjetAtrst.Services;
using ProjetAtrst.ViewModels.ProjectRequests;
using System.Security.Claims;

namespace ProjetAtrst.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(ProfileCompletionFilter))]
    public class ProjectRequestController : Controller
    {
        private readonly IProjectRequestService _requestService;
        private readonly IUserService _userService;

        public ProjectRequestController(IProjectRequestService requestService,IUserService userService)
        {
            _requestService = requestService;
            _userService = userService;
        }

       

      
        [HttpGet]
        public async Task<IActionResult> Send(int projectId, string receiverId, RequestType type)
        {
            var model = await _requestService.PrepareRequestModelAsync(projectId, receiverId, type);
            if (model == null)
                return NotFound();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.IsroleResearche = RoleType.Researcher == await _userService.GetRoleAsync(userId);
            return PartialView("_SendRequestModalPartial", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(ProjectRequestCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _requestService.SendRequestAsync(model, senderId);
            if (model.Type == RequestType.Join) {
                TempData["Success"] = "تم إرسال طلب الانضمام بنجاح.";
                return RedirectToAction("SentJoinRequests", "ProjectRequest");
            }
            else
            {
                TempData["Success"] = "تم إرسال الدعوة بنجاح.";
                return RedirectToAction("SentInvitations", "ProjectRequest");
            };
            
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

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ProjectRequestDetailsPartial", request);
            }
            return View(request);
           
        }

        [HttpGet]
        public async Task<IActionResult> Outgoing()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var requests = await _requestService.GetSentJoinRequestsAsync(userId);
            return View(requests);
        }

        [HttpGet]
        public async Task<IActionResult> Incoming()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var invitations = await _requestService.GetMyInvitationsAsync(userId);
            return View(invitations);
        }
       

    }

}
