using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Controllers;
using ProjetAtrst.Enums;
using ProjetAtrst.Helpers;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Models;
using ProjetAtrst.Services;
using ProjetAtrst.ViewModels.Project;
using ProjetAtrst.ViewModels.Researcher;
using System.Security.Claims;

[Authorize]
public class ProjectContextController : ProjectContextBaseController
{
    private readonly IProjectService _projectService;
    private readonly IProjectRequestService _requestService;
    private readonly IResearcherService _researcherService;
    private readonly IUserService _userService;

   
    public ProjectContextController(IProjectService projectService, IProjectRequestService requestService, IResearcherService researcherService, IUserService userService)
    {
        _projectService = projectService;
        _requestService = requestService;
        _researcherService = researcherService;
        _userService = userService;
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

        // Reload the form to get updated data
        var updatedModel = await _projectService.GetProjectForEditAsync(userId, model.Id);
        ViewData["Title"] = $"Modifier le projet: {updatedModel?.Title ?? model.Title}";

        return View(updatedModel ?? model);
    }

    [HttpGet]
    public async Task<IActionResult> Members()
    {
        var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
        if (projectId == null)
            return RedirectToAction("Enter");
        var members = await _projectService.GetProjectMembersAsync(projectId.Value);
        return View(members);
    }

    [HttpGet]
    public async Task<IActionResult> Requests()
    {
        var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
        if (projectId == null)
            return Forbid();
        var requests = await _projectService.GetJoinRequestsAsync(projectId.Value);
        return View(requests);
    }
    
    [HttpGet]
    public async Task<IActionResult> Invitation()
    {
        var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
        if (projectId == null)
            return RedirectToAction("Enter");
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
        await _requestService.RejectRequestAsync(requestId, RejectionType.JoinRequest);

        TempData["Warning"] = "La demande a été rejetée.";
        return RedirectToAction("Requests");
    }

    [HttpPost]
    public async Task<IActionResult> SendInvitation(string researcherId)
    {
        var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
        if (projectId == null)
            return RedirectToAction("Index");

        await _requestService.SendInvitationAsync(projectId.Value, researcherId);
        TempData["Success"] = "Invitation sent successfully.";
        return RedirectToAction("SendInvitations");
    }

    [HttpGet]
    public async Task<IActionResult> SendInvitations(int draw = 1, int start = 0, int length = 10, string search = null, string sortColumn = null, string sortDirection = null)
    {
        var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
        if (projectId == null)
            return RedirectToAction("Index");

        // For AJAX requests from DataTables
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            var searchValue = Request.Query["search[value]"].FirstOrDefault();
            var orderColumnIndex = Request.Query["order[0][column]"].FirstOrDefault();
            var orderDirection = Request.Query["order[0][dir]"].FirstOrDefault();

            // Determine sort column
            var columnNames = new[] { "profilePicturePath", "fullName", "Role", "id" };
            var columnName = orderColumnIndex != null && int.Parse(orderColumnIndex) < columnNames.Length
                ? columnNames[int.Parse(orderColumnIndex)]
                : "fullName";

            var excludedIds = await _userService.GetInvitedOrMembersIdsAsync(projectId.Value);
            var totalCount = await _userService.GetAvailableUsersCountAsync(excludedIds);
            var filteredCount = await _userService.GetAvailableUsersCountAsync(excludedIds, searchValue);

            var users = await _userService.GetAvailableUsersAsync(excludedIds, start, length, searchValue, columnName, orderDirection);

            var data = users.Select(u => new
            {
                profilePicturePath = string.IsNullOrEmpty(u.ProfilePicturePath)
                    ? "/images/default-project.png"
                    : u.ProfilePicturePath,
                fullName = u.FullName,
                gender = u.RoleType.ToString(),
                id = u.Id
            }).ToList();

            return Json(new
            {
                draw = draw,
                recordsTotal = totalCount,
                recordsFiltered = filteredCount,
                data = data
            });
        }

        // For normal request (initial page load)
        var excludedIdsForInitial = await _userService.GetInvitedOrMembersIdsAsync(projectId.Value);
        var initialUsers = await _userService.GetAvailableUsersAsync(excludedIdsForInitial, 0, 6);
        var totalCountInitial = await _userService.GetAvailableUsersCountAsync(excludedIdsForInitial);

        var viewModel = new SendInvitationViewModel
        {
            Researchers = initialUsers.Select(u => new ResearcherViewModel
            {
                Id = u.Id,
                FullName = u.FullName,
                Gender = u.Gender,
                ProfilePicturePath = u.ProfilePicturePath
            }).ToList(),
            CurrentProjectId = projectId.Value,
            Pagination = new PaginationModel
            {
                CurrentPage = 1,
                TotalPages = (int)Math.Ceiling(totalCountInitial / 6.0)
            }
        };

        return View(viewModel);
    }
}
