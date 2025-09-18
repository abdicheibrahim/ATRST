using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels.Dashboard;
using ProjetAtrst.ViewModels.ProjectTask;
using System.Security.Claims;

namespace ProjetAtrst.Controllers
{
   
    [Authorize]
    public class ProjectTasksController :  ProjectContextBaseController
    {
        private readonly IProjectTaskService _taskService;
       

        public ProjectTasksController(IProjectTaskService taskService, ApplicationDbContext db)
        {
            _taskService = taskService;
           
        }

        public async Task<IActionResult> Index()
        {

            return View();
        }
        // GET: ProjectTasks/Create
        public async Task<IActionResult> Create()
        {
            // Get current project ID from session
            var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
            if (projectId == null)
                return RedirectToAction("Enter", "ProjectContext");

            // Initialize model with default values
            var model = new ProjectTaskViewModel
            {
                ProjectId = projectId.Value,
                StartDate = DateOnly.FromDateTime(DateTime.Today), // Set today as default start date
                Status = "Pending" // Default status
            };

            ViewBag.CurrentProjectId = projectId.Value;
            return PartialView("_CreateTaskPartial.cshtml", model);
        }

        // POST: ProjectTasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectTaskViewModel model)
        {


            // Verify project context still exists
            var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
            if (projectId == null)
                return RedirectToAction("Enter", "ProjectContext");

            // Ensure project ID is set correctly
            model.ProjectId = projectId.Value;

            if (!ModelState.IsValid)
            {
                ViewBag.CurrentProjectId = projectId.Value;
                return View(model);
            }

            try
            {
                var created = await _taskService.CreateAsync(model);
                //if (created)
                //{
                    TempData["SuccessMessage"] = "Tâche créée avec succès";
                    return RedirectToAction(nameof(Index));
                //}
                //else
                //{
                //    ModelState.AddModelError("", "Une erreur s'est produite lors de la création de la tâche");
                //    ViewBag.CurrentProjectId = projectId.Value;
                //    return View(model);
                //}
            }
            catch (Exception ex)
            {
                // Log the exception here if needed
                ModelState.AddModelError("", "Une erreur inattendue s'est produite");
                ViewBag.CurrentProjectId = projectId.Value;
                return View(model);
            }
        }


    }

}
