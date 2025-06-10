using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels;
using ProjetAtrst.ViewModels.UserAccess;
using System.Security.Claims;

namespace ProjetAtrst.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IUserAccessService _userAccessService;
        private readonly IInvitationRequestService _invitationRequestService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectController(
            IProjectService projectService,
            IUserAccessService userAccessService,
            IHttpContextAccessor httpContextAccessor,
            IInvitationRequestService invitationRequestService)
        {
            _projectService = projectService;
            _userAccessService = userAccessService;
            _httpContextAccessor = httpContextAccessor;
            _invitationRequestService = invitationRequestService;
        }

        private string GetUserId() =>
            _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = GetUserId();
            var access = await _userAccessService.GetAccessStatusAsync(userId);

            if (!access.IsCompleted)
            {
                TempData["Error"] = "يجب إكمال بياناتك الشخصية قبل إنشاء مشروع.";
                return RedirectToAction("Index", "Dashboard");
            }

            if (!access.IsApproved)
            {
                TempData["Error"] = "بانتظار موافقة الإدارة قبل أن تتمكن من إنشاء مشروع.";
                return RedirectToAction("Index", "Dashboard");
            }


            return View(new CreateProjectViewModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProjectViewModel model)
        {
            var userId = GetUserId();

            if (!ModelState.IsValid)
                return View(model);

            var success = await _projectService.CreateProjectAsync(userId, model);

            if (!success)
            {
                TempData["Error"] = "لا يمكنك إنشاء مشروع جديد الآن.";
                return RedirectToAction("Index", "Dashboard");
            }

            TempData["Success"] = "تم إنشاء المشروع بنجاح!";
            return RedirectToAction("Index", "Dashboard");
        }
        [HttpGet]
        public async Task<IActionResult> OpenProjects()
        {
            var userId = GetUserId();
            var projects = await _projectService.GetOpenProjectsForJoiningAsync(userId);
            return View(projects);
        }

        [HttpGet]
        public async Task<IActionResult> EligibleMembers(int projectId)
        {
            var members = await _invitationRequestService.GetAllEligibleForInvitationAsync(projectId);
            ViewBag.ProjectId = projectId;
            return View(members);
        }
        [HttpPost]
        public async Task<IActionResult> SendInvitation(string receiverId, int projectId)
        {
            await _invitationRequestService.SendInvitationAsync(receiverId, projectId);
            return RedirectToAction("EligibleMembers", new { projectId });
        }
        public IActionResult EditProject()
        {
            return View();
        }
        public IActionResult Documents()
        {
            return View();
        }
        public IActionResult Partners()
        {
            return View();
        }
        public IActionResult WorkPackages()
        {
            return View();
        }
        public IActionResult Deliverables()
        {
            return View();
        }
    }
}