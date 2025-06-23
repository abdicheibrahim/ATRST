using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.Project;

namespace ProjetAtrst.Interfaces.Services
{
    public interface IProjectService
    {
        //Verified
        Task CreateProjectAsync(ProjectCreateViewModel model, string researcherId);
        Task<List<ProjectListViewModel>> GetProjectsForResearcherAsync(string researcherId);
        Task<ProjectDetailsViewModel?> GetProjectDetailsForResearcherAsync(string researcherId, int projectId);
        Task<ProjectEditViewModel?> GetProjectForEditAsync(string researcherId, int projectId);
        Task<bool> UpdateProjectAsync(string researcherId, ProjectEditViewModel model);
        //Not Verified
        Task<List<AvailableProjectViewModel>> GetAvailableProjectsAsync(string researcherId);
        Task<bool> IsUserLeaderAsync(string researcherId, int projectId);

        //Task<ProjectEditViewModel?> GetLeaderProjectAsync(string researcherId);
        //Task<bool> UpdateLeaderProjectAsync(ProjectEditViewModel model, string researcherId);
        //  Task<ProjectEditViewModel?> GetProjectForEditAsync(int projectId, string researcherId);
        // Task<(ProjectEditViewModel Model, Role Role)?> GetProjectEditViewModelWithRoleAsync(int projectId, string researcherId);

    }
}
