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
            ViewData["Title"] = "Créer un nouveau projet";
            return View(new ProjectCreateViewModel());
        }

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
       
        [HttpGet]
        [AllowAnonymous]
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

        [HttpGet]
        public async Task<IActionResult> MyProjects()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var projects = await _projectService.GetProjectsForResearcherAsync(userId);
            return View(projects);
        }

        //public async Task<IActionResult> Details(int id)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var model = await _projectService.GetProjectDetailsForResearcherAsync(userId, id);

        //    if (model == null)
        //        return NotFound();

        //    return View(model);
        //}

        //public async Task<IActionResult> AvailableProjects(int page = 1, int pageSize = 6)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var (projects, totalCount) = await _projectService.GetAvailableProjectsAsync(userId, page, pageSize);

        //    var paginationModel = new PaginationModel
        //    {
        //        CurrentPage = page,
        //        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
        //        ActionName = nameof(AvailableProjects)
        //    };

        //    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        //    {
        //        return Json(new
        //        {
        //            projects = projects,
        //            pagination = paginationModel
        //        });
        //    }

        //    var viewModel = new AvailableProjectsWithPaginationViewModel
        //    {
        //        Projects = projects,
        //        Pagination = paginationModel
        //    };
        //    return View(viewModel);
        //}

        // View رئيسي (فيه جدول فارغ مبدئياً)

        // يعرض الصفحة فقط (الجدول يكون فاضي ويمتلي عبر DataTables)
        public IActionResult AvailableProjects() => View();

        // يُستدعى من DataTables عبر AJAX (POST)
        [HttpPost]
        public async Task<IActionResult> LoadProjects()
        {
            // باراميترات DataTables القياسية
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

            // استدعاء الخدمة المجهزة لـ DataTables
            var resp = await _projectService.GetAvailableProjectsDataTableAsync(
                researcherId: researcherId,
                start: start,
                length: length,
                searchValue: searchValue,
                sortColumn: sortColumn,
                sortDirection: sortDirection,
                draw: draw
            );

            // الشكل الذي تتوقعه DataTables
            return Json(new
            {
                draw = resp.Draw,
                recordsTotal = resp.RecordsTotal,
                recordsFiltered = resp.RecordsFiltered,
                data = resp.Data
            });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var project = await _projectService.GetProjectDetailsAsync(id);
            if (project == null)
                return NotFound();

            return Ok(project); // سيُرسل JSON للـ client
        }

    }
}
