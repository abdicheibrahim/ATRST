using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.Project;
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

        public async Task<IActionResult> AvailableProjects(int page = 1, int pageSize = 6)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var (projects, totalCount) = await _projectService.GetAvailableProjectsAsync(userId, page, pageSize);

            var paginationModel = new PaginationModel
            {
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                ActionName = nameof(AvailableProjects)
            };

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new
                {
                    projects = projects,
                    pagination = paginationModel
                });
            }

            var viewModel = new AvailableProjectsWithPaginationViewModel
            {
                Projects = projects,
                Pagination = paginationModel
            };
            return View(viewModel);
        }



    }
}
