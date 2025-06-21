//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using ProjetAtrst.Interfaces.Services;
//using ProjetAtrst.Models;
//using ProjetAtrst.Interfaces;
//using ProjetAtrst.ViewModels.ProjectRequests;
//using System.Security.Claims;

//namespace ProjetAtrst.Controllers
//{
//    [Authorize]
//    [ServiceFilter(typeof(ProfileCompletionFilter))]
//    public class ProjectRequestsController : Controller
//    {
//        private readonly IProjectRequestService _projectRequestService;
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly IUnitOfWork _unitOfWork;

//        public ProjectRequestsController(
//            IProjectRequestService projectRequestService,
//            UserManager<ApplicationUser> userManager,
//            IUnitOfWork unitOfWork)
//        {
//            _projectRequestService = projectRequestService;
//            _userManager = userManager;
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var userId = _userManager.GetUserId(User);
//            if (string.IsNullOrEmpty(userId))
//            {
//                return RedirectToAction("Login", "Account");
//            }

//            var joinRequestsToMyProjects = await _projectRequestService.GetJoinRequestsToMyProjectsGroupedAsync(userId);
//            var invitationsISent = await _projectRequestService.GetInvitationsISentAsync(userId);
//            var joinRequestsISent = await _projectRequestService.GetJoinRequestsISentAsync(userId);
//            var invitationsIReceived = await _projectRequestService.GetInvitationsIReceivedAsync(userId);

//            var viewModel = new ProjectRequestsIndexViewModel
//            {
//                JoinRequestsToMyProject = joinRequestsToMyProjects,
//                InvitationsISent = invitationsISent,
//                JoinRequestsISent = joinRequestsISent,
//                InvitationsIReceived = invitationsIReceived
//            };

//            return View(viewModel);
//        }
//        public async Task<IActionResult> SendRequest(JoinRequestCreateViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            var researcher = User.FindFirstValue(ClaimTypes.NameIdentifier);

//            var joinRequest = new JoinRequest
//            {
//                ResearcherId = researcher,
//                ProjectId = model.ProjectId,
//                Message = model.Message
//            };

//            await _unitOfWork.JoinRequests.CreateAsync(joinRequest);
//            await _unitOfWork.SaveAsync();

//            // إرسال إشعار إلى قائد المشروع
//            var leaderId = await _unitOfWork.Projects. GetLeaderUserIdAsync(model.ProjectId);
//            await _notificationService.CreateNotificationAsync(
//                leaderId,
//                "طلب انضمام جديد",
//                $"طلب الباحث {researcher.FullName} الانضمام إلى مشروعك. تحقق من الطلب.",
//                NotificationType.JoinRequestSent,
//                joinRequest.Id // أو ProjectId و ResearcherId في JSON إن أردت
//            );

//            TempData["Success"] = "تم إرسال الطلب بنجاح!";
//            return RedirectToAction("Details", "Project", new { id = model.ProjectId });
//        }
//    }
//}
