using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using ProjetAtrst.Helpers;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels.Project;
using System.Security.Claims;

namespace ProjetAtrst.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(ProfileCompletionFilter))]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly StaticDataLoader _staticDataLoader;
        // private readonly IProjectRequestService _projectRequestService;
        public ProjectController(IProjectService projectService, StaticDataLoader staticDataLoader)
        {
            _projectService = projectService;
            _staticDataLoader = staticDataLoader;
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"] = "Create a new project";

            var model = new ProjectCreateViewModel
            {
                // لم نعد بحاجة لتحميل القوائم هنا لأن التحميل صار عبر AJAX
                //DomainsList = default,
                //AxesList = default,
                //ThemesList = default,
                //NaturesList = default,
                //TRLLevelsList = default,
                //PNRList = default,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // لم نعد بحاجة لإعادة تعبئة القوائم لأن الـ Select2 يعمل بالـ AJAX
                //model.DomainsList = default;
                //model.AxesList = default;
                //model.ThemesList = default;
                //model.NaturesList = default;
                //model.TRLLevelsList = default;
                //model.PNRList = default;

                return View(model);
            }

            var researcherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(researcherId))
                return Forbid();

            await _projectService.CreateProjectAsync(model, researcherId);
            TempData["SuccessMessage"] = "Le projet a été créé avec succès.";
            
           
            return RedirectToAction("MyProjects");
        }

        // نقطة البحث الموحدة لكل القوائم
        [HttpGet]
        public IActionResult SearchDropdown(string type, string term, int take = 25)
        {
            if (string.IsNullOrWhiteSpace(type))
                return BadRequest("Missing type");

            var list = _staticDataLoader.SearchList(type, term, take);

            // Select2 يتوقع id/text
            var data = list.Select(x => new { id = x.Value, text = x.Text }).ToList();

            return Json(data);
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
