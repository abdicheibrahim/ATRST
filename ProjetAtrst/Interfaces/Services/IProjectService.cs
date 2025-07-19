using ProjetAtrst.ViewModels.ProjectRequests;
using ProjetAtrst.ViewModels.Project;
using ProjetAtrst.ViewModels.ProjectMembership;
using ProjetAtrst.Models;
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
        Task<(List<AvailableProjectViewModel> Projects, int TotalCount)> GetAvailableProjectsAsync(string researcherId, int pageNumber, int pageSize);
        Task<bool> IsUserLeaderAsync(string researcherId, int projectId);
        Task<List<ProjectMemberViewModel>> GetProjectMembersAsync(int projectId);
        Task<List<ProjectJoinRequestViewModel>> GetJoinRequestsAsync(int projectId);
        Task<List<ProjectJoinRequestViewModel>> GetInvitationRequestsAsync(int projectId);
        Task<Project> GetByIdAsync(int id);
        
        //Task<ProjectEditViewModel?> GetLeaderProjectAsync(string researcherId);
        //Task<bool> UpdateLeaderProjectAsync(ProjectEditViewModel model, string researcherId);
        //  Task<ProjectEditViewModel?> GetProjectForEditAsync(int projectId, string researcherId);
        // Task<(ProjectEditViewModel Model, Role Role)?> GetProjectEditViewModelWithRoleAsync(int projectId, string researcherId);

    }
}
