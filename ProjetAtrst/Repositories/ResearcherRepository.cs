using ProjetAtrst.Models;
using ProjetAtrst.Interfaces.Repositories;
using System.Linq;

namespace ProjetAtrst.Repositories
{
    public class ResearcherRepository : GenericRepository<Researcher>, IResearcherRepository
    {
        public ResearcherRepository(ApplicationDbContext context) : base(context) { }

        //------------New Code----------------
        //public async Task<List<Researcher>> GetAvailableResearchersForInvitationAsync(int projectId)
        //{
        //    var invitedResearchers = await _context.ProjectRequests
        //        .Where(pr => pr.ProjectId == projectId && pr.Type == RequestType.Invitation)
        //        .Select(pr => pr.ReceiverId)
        //        .ToListAsync();

        //    var currentMembers = await _context.ProjectMemberships
        //        .Where(pm => pm.ProjectId == projectId)
        //        .Select(pm => pm.ResearcherId)
        //        .ToListAsync();

        //    return await _context.Researchers
        //        .Where(r => !invitedResearchers.Contains(r.Id) && !currentMembers.Contains(r.Id)).Include(s => s.User)
        //        .ToListAsync();
        //}

        public async Task<List<string>> GetInvitedOrMembersIdsAsync(int projectId)
        {
            var invited = await _context.ProjectRequests
                .Where(pr => pr.ProjectId == projectId && pr.Type == RequestType.Invitation)
                .Select(pr => pr.ReceiverId)
                .ToListAsync();

            var members = await _context.ProjectMemberships
                .Where(pm => pm.ProjectId == projectId)
                .Select(pm => pm.UserId)
                .ToListAsync();

            return invited.Union(members).ToList();
        }

        public async Task<List<Researcher>> GetAvailableResearchersAsync(List<string> excludedIds, int page, int pageSize)
        {
            return await _context.Researchers
                .Where(r => !excludedIds.Contains(r.Id))
                .Include(r => r.User)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetAvailableResearchersCountAsync(List<string> excludedIds)
        {
            return await _context.Researchers
                .CountAsync(r => !excludedIds.Contains(r.Id));
        }
    }

}

