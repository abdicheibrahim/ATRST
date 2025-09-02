using Microsoft.EntityFrameworkCore;
using ProjetAtrst.Models;
using ProjetAtrst.Repositories;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IResearcherRepository : IGenericRepository<Researcher>
    {
      
        Task<List<string>> GetInvitedOrMembersIdsAsync(int projectId);
        Task<List<Researcher>> GetAvailableResearchersAsync(List<string> excludedIds, int page, int pageSize);
        Task<int> GetAvailableResearchersCountAsync(List<string> excludedIds);

    }
}

