using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetAtrst.Interfaces.Services;
using System.Security.Claims;

namespace ProjetAtrst.Controllers
{
    [Authorize]
    public class ProjectTaskAssignmentsController : ProjectContextBaseController
    {
        private readonly IProjectTaskAssignmentService _assignmentService;
        private readonly IProjectService _project;
        //private readonly UserManager<ApplicationUser> _userManager;

        public ProjectTaskAssignmentsController(
            IProjectTaskAssignmentService service,
                IProjectService project,
            UserManager<ApplicationUser> userManager)
        {
            _assignmentService = service;
            _project = project;
            //_userManager = userManager;
        }

        // GET: Assign User (modal form)
        public async Task<IActionResult> Assign(int taskId)
        {
            var projectId = HttpContext.Session.GetInt32("CurrentProjectId");
            if (projectId == null)
                return RedirectToAction("Enter", "ProjectContext");

            var users = await _project.GetProjectMembersAsync(projectId.Value);
            //ViewBag.Users = new SelectList(users, "Id", "FullName");
            ViewBag.Users = new SelectList(users, "UserId", "Name");
            ViewBag.TaskId = taskId;
            return PartialView("_AssignUserPartial");
        }

        // POST: Assign User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(int taskId, string userId, string role)
        {
            try
            {
                await _assignmentService.AssignUserAsync(taskId, userId, role);
                return Json(new { success = true, message = "Utilisateur assigné avec succès" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Remove assignment
        [HttpPost]
        public async Task<IActionResult> Remove(int assignmentId)
        {
            await _assignmentService.RemoveAssignmentAsync(assignmentId);
            return RedirectToAction("Index", "ProjectTasks");
        }


      
    }

}
