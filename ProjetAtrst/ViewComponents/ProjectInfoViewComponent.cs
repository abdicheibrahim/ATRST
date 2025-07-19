using Microsoft.AspNetCore.Mvc;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.ViewModels.ViewComponent;
namespace ProjetAtrst.ViewComponents
{
    public class ProjectInfoViewComponent : ViewComponent
    {
        private readonly IProjectService _projectService;
        private readonly IProjectRepository _ProjectRepository;

        public ProjectInfoViewComponent(IProjectService projectService, IProjectRepository ProjectRepository)
        {
            _projectService = projectService;
            _ProjectRepository = ProjectRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var projectId = HttpContext.Session.GetInt32("CurrentProjectId");

           // if (projectId == null)
               // return View("Default", null);

            var project = await _ProjectRepository.GetByIdAsync(projectId.Value);

//            if (project == null)
  //              return View("Default", null);

            var model = new ProjectInfoViewModel
            {
                ProjectId = project.Id,
                Title = project.Title,
                LogoPath = project.LogoPath
            };

            return View("Default", model);
        }
    }

   
}
