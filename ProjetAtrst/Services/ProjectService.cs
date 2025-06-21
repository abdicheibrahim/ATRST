using ProjetAtrst.Interfaces;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.Project;

namespace ProjetAtrst.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateProjectAsync(ProjectCreateViewModel model, string researcherId)
        {
            var project = new Project
            {
                Title = model.Title,
                Description = model.Description,
                CreationDate = DateTime.UtcNow,
                LastActivity = DateTime.UtcNow,
                ProjectApprovalStatus = ProjectApprovalStatus.Pending,
                ProjectStatus = ProjectStatus.Open,
                IsCompleted = false,
                IsAcceptingJoinRequests = true,
                ProjectMemberships = new List<ProjectMembership>()
            };

            await _unitOfWork.Projects.AddAsync(project);
            await _unitOfWork.SaveAsync();

            var membership = new ProjectMembership
            {
                ProjectId = project.Id, 
                ResearcherId = researcherId,
                Role = Role.Leader,
                JoinedAt = DateTime.UtcNow
            };

            await _unitOfWork.ProjectMemberships.AddAsync(membership);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<ProjectListViewModel>> GetProjectsForResearcherAsync(string researcherId)
        {
            var memberships = await _unitOfWork.ProjectMemberships
                .GetAllByResearcherWithProjectsAsync(researcherId);

            return memberships.Select(pm => new ProjectListViewModel
            {
                Id = pm.Project.Id,
                Title = pm.Project.Title,
                CreationDate = pm.Project.CreationDate,
                Status = pm.Project.ProjectStatus.ToString(),
                LastActivity = pm.Project.LastActivity,
                Role = pm.Role switch
                {
                    Role.Leader => Role.Leader,
                    Role.Member => Role.Member,
                    _ => Role.Viewer,

                }
            }).ToList();
        }
        public async Task<ProjectDetailsViewModel?> GetProjectDetailsForResearcherAsync(string researcherId, int projectId)
        {
            var membership = await _unitOfWork.ProjectMemberships
                .GetByResearcherAndProjectAsync(researcherId, projectId);

            if (membership == null) return null;

            var project = membership.Project;

            return new ProjectDetailsViewModel
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                CreationDate = project.CreationDate,
                Status = project.ProjectStatus.ToString(),
                Role = membership.Role 
            };
        }

       
        public async Task<ProjectEditViewModel?> GetProjectForEditAsync(string researcherId, int projectId)
        {
            var membership = await _unitOfWork.ProjectMemberships
                .GetByResearcherAndProjectAsync(researcherId, projectId);

            if (membership == null || membership.Role != Role.Leader)
                return null;

            var project = membership.Project;

            return new ProjectEditViewModel
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description
            };
        }

        public async Task<bool> UpdateProjectAsync(string researcherId, ProjectEditViewModel model)
        {
            var membership = await _unitOfWork.ProjectMemberships
                .GetByResearcherAndProjectAsync(researcherId, model.Id);

            if (membership == null || membership.Role != Role.Leader)
                return false;

            var project = membership.Project;
            project.Title = model.Title;
            project.Description = model.Description;
            project.LastActivity = DateTime.UtcNow;

            await _unitOfWork.SaveAsync();
            return true;
        }

        //Not Verified

        public async Task<List<AvailableProjectViewModel>> GetAvailableProjectsAsync(string researcherId)
        {
            var projects = await _unitOfWork.Projects.GetAvailableProjectsForJoinAsync(researcherId);

            return projects.Select(p => new AvailableProjectViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                CreationDate = p.CreationDate,
                LeaderFullName = p.ProjectMemberships
                    .Where(pm => pm.Role == Role.Leader)
                    .Select(pm => pm.Researcher.User.FullName)
                    .FirstOrDefault() ?? "غير معروف"
            }).ToList();
        }


    }
}
