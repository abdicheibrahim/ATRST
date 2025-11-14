using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels.ProjectTask;
using System.Security.Claims;
namespace ProjetAtrst.Controllers
{

    [Authorize]
    public class ProjectTasksController : ProjectContextBaseController
    {
        private readonly IProjectTaskService _taskService;


        public ProjectTasksController(IProjectTaskService taskService, ApplicationDbContext db)
        {
            _taskService = taskService;

        }

        public async Task<IActionResult> Index()
        {
            var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
            if (projectId == null)
                return RedirectToAction("Enter", "ProjectContext");

            var tasks = await _taskService.GetTasksByProjectIdAsync(projectId.Value);
            ViewBag.CurrentProjectId = projectId.Value;
            return View(tasks);
        }

        // GET: ProjectTasks/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            return View(task);
        }
        // GET: ProjectTasks/Create
        public async Task<IActionResult> Create()
        {
            var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
            if (projectId == null)
                return RedirectToAction("Enter", "ProjectContext");

           
            var model = new ProjectTaskViewModel
            {
                ProjectId = projectId.Value,
                StartDate = DateOnly.FromDateTime(DateTime.Today), 
                Status = "Pending" 
            };

            ViewBag.CurrentProjectId = projectId.Value;
            return PartialView("_CreateTaskPartial.cshtml", model);
        }

        // POST: ProjectTasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectTaskViewModel model)
        {
            var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
            if (projectId == null)
                return RedirectToAction("Enter", "ProjectContext");

            model.ProjectId = projectId.Value;

            if (!ModelState.IsValid)
            {
                ViewBag.CurrentProjectId = projectId.Value;
                return PartialView("_CreateTaskPartial", model);
            }
            try
            {
                var created = await _taskService.CreateAsync(model);

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    if (created)
                        return Json(new { success = true, message = "Tâche créée avec succès" });
                    else
                        return Json(new { success = false, message = "Erreur lors de la création" });
                }

                if (created)
                {
                    TempData["SuccessMessage"] = "Tâche créée avec succès";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Une erreur s'est produite lors de la création de la tâche");
                    ViewBag.CurrentProjectId = projectId.Value;
                    return PartialView("_CreateTaskPartial", model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Une erreur inattendue s'est produite");
                ViewBag.CurrentProjectId = projectId.Value;

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Erreur inattendue" });
                }

                return PartialView("_CreateTaskPartial", model);
            }
        }

        // GET: ProjectTasks/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
            if (projectId == null)
                return RedirectToAction("Enter", "ProjectContext");

            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            if (task.ProjectId != projectId.Value)
                return Forbid();
            ViewBag.TaskId = id;
            return PartialView("_EditTaskPartial", task);
        }

        // POST: ProjectTasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectTaskViewModel model)
        {
            var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
            if (projectId == null)
                return RedirectToAction("Enter", "ProjectContext");

            
            if (!ModelState.IsValid)
            {
                return PartialView("_EditTaskPartial", model);
            }

            try
            {
                var updated = await _taskService.UpdateAsync(id, model);

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    if (updated)
                        return Json(new { success = true, message = "Tâche mise à jour avec succès" });
                    else
                        return Json(new { success = false, message = "Erreur lors de la mise à jour" });
                }

                if (updated)
                {
                    TempData["SuccessMessage"] = "Tâche mise à jour avec succès";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Une erreur s'est produite lors de la mise à jour");
                    return PartialView("_EditTaskPartial", model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Une erreur inattendue s'est produite");

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Erreur inattendue" });
                }

                return PartialView("_EditTaskPartial", model);
            }
        }


        // POST: ProjectTasks/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
            if (projectId == null)
                return RedirectToAction("Enter", "ProjectContext");

            try
            {
                
                var task = await _taskService.GetTaskByIdAsync(id);
                if (task == null || task.ProjectId != projectId.Value)
                {
                    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        return Json(new { success = false, message = "Tâche non trouvée" });
                    }
                    TempData["ErrorMessage"] = "Tâche non trouvée";
                    return RedirectToAction(nameof(Index));
                }

                var deleted = await _taskService.DeleteAsync(id);

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    if (deleted)
                        return Json(new { success = true, message = "Tâche supprimée avec succès" });
                    else
                        return Json(new { success = false, message = "Erreur lors de la suppression" });
                }

                if (deleted)
                {
                    TempData["SuccessMessage"] = "Tâche supprimée avec succès";
                }
                else
                {
                    TempData["ErrorMessage"] = "Une erreur s'est produite lors de la suppression";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Une erreur inattendue s'est produite";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ProjectTasks/GetTasks
        public async Task<IActionResult> GetTasks()
        {
            var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
            if (projectId == null)
                return Json(new { success = false, message = "Projet non trouvé" });

            try
            {
                var tasks = await _taskService.GetTasksByProjectIdAsync(projectId.Value);
                return Json(new { success = true, tasks });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erreur lors du chargement des tâches" });
            }
        }

      

    }
}
