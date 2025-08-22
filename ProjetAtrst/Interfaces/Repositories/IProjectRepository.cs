using ProjetAtrst.DTO;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<Project?> GetByIdAsync(int id);
        Task<(string ProjectTitle, string LeaderFullName)> GetProjectInfoAsync(int projectId);
        Task<(List<AvailableProjectDto> Projects, int TotalCount)>GetAvailableProjectsForJoinAsync(string researcherId, int pageNumber, int pageSize);
        IQueryable<AvailableProjectDto> GetAvailableProjectsQuery(string researcherId);
        Task<ProjectDetailsDto?> GetProjectDetailsAsync(int projectId);

        //---New methods for project management---
        Task<Project?> GetProjectForEditAsync(int projectId);
        Task<bool> UpdateProjectAsync(Project project);
        Task<bool> IsUserProjectLeaderAsync(int projectId, string userId);
        Task<Project?> GetProjectWithMembersAsync(int projectId);
    }
}
