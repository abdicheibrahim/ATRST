using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Models;
using ProjetAtrst.Services;
using ProjetAtrst.ViewModels.Project;
using ProjetAtrst.ViewModels.ProjectRequests;
using System.Security.Claims;

namespace ProjetAtrst.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(ProfileCompletionFilter))]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
       // private readonly IProjectRequestService _projectRequestService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
           // _projectRequestService = projectRequestService;
        }

        //Verified

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var researcherId = User.FindFirstValue(ClaimTypes.NameIdentifier); // أو من Session
            await _projectService.CreateProjectAsync(model, researcherId);

            return RedirectToAction("MyProjects");
        }
        [HttpGet]
        public async Task<IActionResult> MyProjects()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var projects = await _projectService.GetProjectsForResearcherAsync(userId);
            return View(projects);
        }

        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = await _projectService.GetProjectDetailsForResearcherAsync(userId, id);

            if (model == null)
                return NotFound();

            return View(model);
        }


        //Not Verified


       // [AuthorizeProjectLeader]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = await _projectService.GetProjectForEditAsync(userId, id);

            if (model == null)
                return Forbid();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProjectEditViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
                return View(model);

            var success = await _projectService.UpdateProjectAsync(userId, model);
            if (!success)
                return Forbid();

            TempData["Success"] = "تم تعديل المشروع بنجاح";
            return RedirectToAction("Details", new { id = model.Id });
        }


        public async Task<IActionResult> AvailableProjects()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var projects = await _projectService.GetAvailableProjectsAsync(userId);
            return View(projects);
        }

        //[HttpPost]
        //public async Task<IActionResult> SendJoinRequest(int projectId)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    try
        //    {
        //        await _projectRequestService.SendJoinRequestAsync(projectId, userId);
        //        TempData["Success"] = "تم إرسال طلب الانضمام بنجاح.";
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        TempData["Error"] = ex.Message;
        //    }

        //    return RedirectToAction("Index");
        //}

       
       

    }
}
