using ProjetAtrst.Models;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IProjectMembershipRepository : IGenericRepository<ProjectMembership>
    {
        Task<IEnumerable<ProjectMembership>> GetAllByUserWithProjectsAsync(string userId);
        Task<ProjectMembership?> GetByResearcherAndProjectAsync(string researcherId, int projectId);

        Task<bool> IsUserLeaderAsync(string researcherId, int projectId);

        Task<List<ProjectMembership>> GetMembersByProjectIdAsync(int projectId);

        Task<List<ProjectMembership>> GetProjectsByUserWithDetailsAsync(string userId);

        Task<int> CountProjectsByUserIdAsync(string researcherId);
    }
}
