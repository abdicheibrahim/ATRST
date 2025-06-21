using Microsoft.EntityFrameworkCore;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Models;

namespace ProjetAtrst.Repositories
{
    public class ProjectMembershipRepository : GenericRepository<ProjectMembership>, IProjectMembershipRepository
    {
        public ProjectMembershipRepository (ApplicationDbContext context) : base(context) { }
        
        //Verified
        public async Task<IEnumerable<ProjectMembership>> GetAllByResearcherWithProjectsAsync(string researcherId)
        {
            return await _context.ProjectMemberships
                .Where(pm => pm.ResearcherId == researcherId)
                .Include(pm => pm.Project)
                .ToListAsync();
        }
        public async Task<ProjectMembership?> GetByResearcherAndProjectAsync(string researcherId, int projectId)
        {
            return await _context.ProjectMemberships
                .Include(pm => pm.Project)
                .FirstOrDefaultAsync(pm => pm.ResearcherId == researcherId && pm.ProjectId == projectId);
        }
        // Not Verified

        public async Task<List<ProjectMembership>> GetProjectsByResearcherWithDetailsAsync(string researcherId)
        {
            return await _context.ProjectMemberships
                .Where(pm => pm.ResearcherId == researcherId)
                .Include(pm => pm.Project)
                    .ThenInclude(p => p.ProjectMemberships)
                        .ThenInclude(m => m.Researcher)
                            .ThenInclude(r => r.User)
                .Include(pm => pm.Project.JoinRequests)
                .Include(pm => pm.Project.SentInvitations)
                .ToListAsync();
        }



    }
}
