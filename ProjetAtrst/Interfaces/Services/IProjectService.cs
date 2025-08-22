using ProjetAtrst.ViewModels.ProjectRequests;
using ProjetAtrst.ViewModels.Project;
using ProjetAtrst.ViewModels.ProjectMembership;
using ProjetAtrst.Models;
using ProjetAtrst.Helpers;
namespace ProjetAtrst.Interfaces.Services
{
    public interface IProjectService
    {
        Task CreateProjectAsync(ProjectCreateViewModel model, string researcherId);
        Task<List<ProjectListViewModel>> GetProjectsForResearcherAsync(string researcherId);
        Task<ProjectDetailsViewModel?> GetProjectDetailsForResearcherAsync(string researcherId, int projectId);
        Task<ProjectEditViewModel?> GetProjectForEditAsync(string researcherId, int projectId);
        Task<bool> UpdateProjectAsync(string researcherId, ProjectEditViewModel model);
        Task<bool> IsUserLeaderAsync(string researcherId, int projectId);
        Task<List<ProjectMemberViewModel>> GetProjectMembersAsync(int projectId);
        Task<List<ProjectJoinRequestViewModel>> GetJoinRequestsAsync(int projectId);
        Task<List<ProjectJoinRequestViewModel>> GetInvitationRequestsAsync(int projectId);
        Task<Project> GetByIdAsync(int id);

        //-- for get available projects for join--//
        Task<(List<AvailableProjectViewModel> Projects, int TotalCount)> GetAvailableProjectsPageAsync(string researcherId, int pageNumber, int pageSize);

        Task<DataTableResponse<AvailableProjectViewModel>> GetAvailableProjectsDataTableAsync(
            string researcherId,
            int start,
            int length,
            string? searchValue,
            string? sortColumn,
            string? sortDirection,
            string? draw 
        );

        Task<ProjectDetailsV2ViewModel?> GetProjectDetailsAsync(int projectId);
        //-------//
    }
}
