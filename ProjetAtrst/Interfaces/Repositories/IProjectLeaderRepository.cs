using ProjetAtrst.Models;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IProjectLeaderRepository : IGenericRepository<ProjectLeader>
    {
        Task<ProjectLeader> GetProjectLeadersByResearcherIdAsync(string researcherId);
    }
}
