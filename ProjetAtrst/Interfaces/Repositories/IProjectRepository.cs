using ProjetAtrst.Models;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        //Task<Project?> GetLeaderProjectAsync(string researcherId);
        Task<Project?> GetByIdAsync(int id);
        Task<(List<Project> Projects, int TotalCount)> GetAvailableProjectsForJoinAsync(string researcherId, int pageNumber, int pageSize);

        Task<(string ProjectTitle, string LeaderFullName)> GetProjectInfoAsync(int projectId);


    }
}
