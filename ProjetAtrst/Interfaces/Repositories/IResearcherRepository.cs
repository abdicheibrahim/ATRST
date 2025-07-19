using ProjetAtrst.Models;
using ProjetAtrst.Repositories;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IResearcherRepository : IGenericRepository<Researcher>
    {
        Task<List<Researcher>> GetAvailableResearchersForInvitationAsync(int projectId);

    }
}
