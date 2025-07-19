using ProjetAtrst.Models;
using ProjetAtrst.Interfaces.Repositories;

namespace ProjetAtrst.Repositories
{
    public class ResearcherRepository : GenericRepository<Researcher>, IResearcherRepository
    {
        public ResearcherRepository(ApplicationDbContext context) : base(context) { }

        //------------New Code----------------
        public async Task<List<Researcher>> GetAvailableResearchersForInvitationAsync(int projectId)
        {
            var invitedResearchers = await _context.ProjectRequests
                .Where(pr => pr.ProjectId == projectId && pr.Type == RequestType.Invitation)
                .Select(pr => pr.ReceiverId)
                .ToListAsync();

            var currentMembers = await _context.ProjectMemberships
                .Where(pm => pm.ProjectId == projectId)
                .Select(pm => pm.ResearcherId)
                .ToListAsync();

            return await _context.Researchers
                .Where(r => !invitedResearchers.Contains(r.Id) && !currentMembers.Contains(r.Id)).Include(s => s.User)
                .ToListAsync();
        }


    }

}

