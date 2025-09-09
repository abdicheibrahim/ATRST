using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using ProjetAtrst.Helpers;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels.Project;
using System.Security.Claims;

namespace ProjetAtrst.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly StaticDataLoader _staticDataLoader;
        public ProjectController(IProjectService projectService, StaticDataLoader staticDataLoader)
        {
            _projectService = projectService;
            _staticDataLoader = staticDataLoader;
        }

        [ServiceFilter(typeof(ProfileCompletionFilter))]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"] = "Créer un nouveau projet";
            return View(new ProjectCreateViewModel());
        }

        [ServiceFilter(typeof(ProfileCompletionFilter))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var researcherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(researcherId))
                    return Forbid();

                await _projectService.CreateProjectAsync(model, researcherId);

                TempData["SuccessMessage"] = "Le projet a été créé avec succès.";
                return RedirectToAction("MyProjects");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Une erreur s'est produite lors de la création du projet. Veuillez réessayer.");
                return View(model);
            }
        }



        [ServiceFilter(typeof(ProfileCompletionFilter))]
        [HttpGet]
        public async Task<IActionResult> MyProjects()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.CanCreateProject = await _projectService.CanUserCreateProjectAsync(userId);

            var projects = await _projectService.GetProjectsForResearcherAsync(userId);
            return View(projects);
        }



        [HttpGet]
        public IActionResult SearchDropdown(string type, string term, int take = 25)
        {
            if (string.IsNullOrWhiteSpace(type))
                return BadRequest("Missing type");

            if (take <= 0) take = 25;
            if (take > 100) take = 100;

            try
            {
                var list = _staticDataLoader.SearchList(type, term, take);
                var data = list.Select(x => new { id = x.Value, text = x.Text }).ToList();
                return Json(data);
            }
            catch
            {
                return Json(new List<object>());
            }
        }

        // Displays only the page (the table is blank and filled via DataTables)
        [ServiceFilter(typeof(ProfileCompletionFilter))]
        public IActionResult AvailableProjects() => View();
        // Called from DataTables via AJAX (POST)
        [HttpPost]
        public async Task<IActionResult> LoadProjects()
        {
           
            var draw = Request.Form["draw"].FirstOrDefault();

            int start = int.TryParse(Request.Form["start"].FirstOrDefault(), out var s) ? s : 0;
            int length = int.TryParse(Request.Form["length"].FirstOrDefault(), out var l) ? l : 10;

            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var orderColIndex = Request.Form["order[0][column]"].FirstOrDefault();
            var sortColumn = !string.IsNullOrEmpty(orderColIndex)
                ? Request.Form[$"columns[{orderColIndex}][name]"].FirstOrDefault()
                : null;

            var sortDirection = Request.Form["order[0][dir]"].FirstOrDefault(); // "asc" | "desc"

            var researcherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(researcherId))
                return Unauthorized();

            var resp = await _projectService.GetAvailableProjectsDataTableAsync(
                researcherId: researcherId,
                start: start,
                length: length,
                searchValue: searchValue,
                sortColumn: sortColumn,
                sortDirection: sortDirection,
                draw: draw
            );

            return Json(new
            {
                draw = resp.Draw,
                recordsTotal = resp.RecordsTotal,
                recordsFiltered = resp.RecordsFiltered,
                data = resp.Data
            });
        }


        [ServiceFilter(typeof(ProfileCompletionFilter))]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var project = await _projectService.GetProjectDetailsAsync(id);
            if (project == null)
                return NotFound();

            return PartialView("_ProjectDetails", project);
        }


    }
}
