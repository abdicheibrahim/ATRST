using ProjetAtrst.Models;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IProjectMembershipRepository : IGenericRepository<ProjectMembership>
    {
        Task<IEnumerable<ProjectMembership>> GetAllByResearcherWithProjectsAsync(string researcherId);
        Task<ProjectMembership?> GetByResearcherAndProjectAsync(string researcherId, int projectId);

        //Not Verified
        Task<List<ProjectMembership>> GetProjectsByResearcherWithDetailsAsync(string researcherId);

    }
}
