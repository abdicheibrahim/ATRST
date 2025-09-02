using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Controllers;
using ProjetAtrst.Enums;
using ProjetAtrst.Helpers;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.Project;
using ProjetAtrst.ViewModels.Researcher;
using System.Security.Claims;

public class ProjectContextController : ProjectContextBaseController
{
    private readonly IProjectService _projectService;
    private readonly IProjectRequestService _requestService;
    private readonly IResearcherService _researcherService;
   
    public ProjectContextController(IProjectService projectService, IProjectRequestService requestService, IResearcherService researcherService)
    {
        _projectService = projectService;
        _requestService = requestService;
        _researcherService = researcherService;
      
    }

    public IActionResult Enter(int projectId)
    {
        HttpContext.Session.SetInt32("CurrentProjectId", projectId);
        return RedirectToAction("Index");
    }

    public async Task< IActionResult> Index()
    {
        var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
        if (projectId == null)
            return RedirectToAction("Index"); 

        var project = await _projectService.GetByIdAsync(projectId.Value);
        if (project == null) return NotFound();

        ViewBag.CurrentProject = project;
        ViewBag.CurrentProjectId = projectId;
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        var projectId = (int?)ViewBag.CurrentProjectId;
        if (projectId == null) return RedirectToAction("Index");

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Forbid();

        var model = await _projectService.GetProjectForEditAsync(userId, projectId.Value);
        if (model == null) return Forbid();

        ViewData["Title"] = $"Modifier le projet: {model.Title}";
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProjectEditViewModel model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Forbid();

        if (!ModelState.IsValid)
        {
            ViewData["Title"] = $"Modifier le projet: {model.Title}";
            return View(model);
        }

        var success = await _projectService.UpdateProjectAsync(userId, model);
        if (!success) return Forbid();

        TempData["ShowSuccessModal"] = true;
        TempData["SuccessTitle"] = "Parfait !";
        TempData["SuccessMessage"] = "Les données ont été modifiées avec succès.";

        // إعادة تحميل النموذج للحصول على البيانات المحدثة
        var updatedModel = await _projectService.GetProjectForEditAsync(userId, model.Id);
        ViewData["Title"] = $"Modifier le projet: {updatedModel?.Title ?? model.Title}";

        return View(updatedModel ?? model);
    }

    [HttpGet]
    public async Task<IActionResult> Members()
    {
        var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
        if (projectId == null)
            return RedirectToAction("Index");
        var members = await _projectService.GetProjectMembersAsync(projectId.Value);
        return View(members);
    }

    [HttpGet]
    public async Task<IActionResult> Requests()
    {
        var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
        if (projectId == null)
            return RedirectToAction("Index");
        var requests = await _projectService.GetJoinRequestsAsync(projectId.Value);
        return View(requests);
    }
    
    [HttpGet]
    public async Task<IActionResult> Invitation()
    {
        var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
        if (projectId == null)
            return RedirectToAction("Index");
        ViewBag.CurrentProjectId = projectId.Value;
        var requests = await _projectService.GetInvitationRequestsAsync(projectId.Value);
        return View(requests);
    }

    [HttpPost]
    public async Task<IActionResult> AcceptRequest(int requestId)
    {
        var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
        if (projectId == null)
            return RedirectToAction("Index");

        await _requestService.AcceptRequestAsync(requestId);
        TempData["ShowSuccessModal"] = true;
        TempData["SuccessTitle"] = "Parfait !";
        TempData["SuccessMessage"] = "Les données ont été modifiées avec succès.";
        return RedirectToAction("Requests");
    }

    [HttpPost]
    public async Task<IActionResult> RejectRequest(int requestId)
    {
        await _requestService.RejectRequestAsync(requestId, RejectionType.Invitation);

        TempData["Warning"] = "تم رفض الطلب.";
        return RedirectToAction("Requests");
    }

    [HttpGet]
    public async Task<IActionResult> SendInvitations(int page = 1, int pageSize = 6)
    {
        var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
        if (projectId == null)
            return RedirectToAction("Index");

        var (researchers, totalCount) = await _researcherService.GetAvailableResearchersForInvitationAsync(projectId.Value, page, pageSize);

        var paginationModel = new PaginationModel
        {
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return Json(new { researchers, pagination = paginationModel });
        }

        var viewModel = new SendInvitationViewModel
        {
            Researchers = researchers,
            Pagination = paginationModel,
            CurrentProjectId = projectId.Value
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> SendInvitation(string researcherId)
    {
        var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
        if (projectId == null)
            return RedirectToAction("Index");

        await _requestService.SendInvitationAsync(projectId.Value, researcherId);
        TempData["Success"] = "تم إرسال الدعوة بنجاح.";
        return RedirectToAction("SendInvitations");
    }

    
}
