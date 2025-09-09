using Microsoft.EntityFrameworkCore;
using ProjetAtrst.Models;
using ProjetAtrst.Repositories;
using ProjetAtrst.ViewModels.Partner;
using ProjetAtrst.ViewModels.Researcher;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IResearcherRepository : IGenericRepository<Researcher>
    {
      
        Task<List<string>> GetInvitedOrMembersIdsAsync(int projectId);
        Task<List<Researcher>> GetAvailableResearchersAsync(List<string> excludedIds, int page, int pageSize);
        Task<int> GetAvailableResearchersCountAsync(List<string> excludedIds);
        Task<ResearcherDetailsViewModel?> GetPartnerDetailsAsync(string PartnerId);

    }
}

