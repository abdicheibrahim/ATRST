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
                .Include(pm => pm.Project.ProjectRequests)
                .ToListAsync();
        }

        public async Task<bool> IsUserLeaderAsync(string researcherId, int projectId)
        {
            return await _context.ProjectMemberships.AnyAsync(m =>
                m.ProjectId == projectId &&
                m.ResearcherId == researcherId &&
                m.Role == Role.Leader);
        }

        public async Task<List<ProjectMembership>> GetMembersByProjectIdAsync(int projectId)
        {
            return await _context.ProjectMemberships
                .Include(pm => pm.Researcher)
                .ThenInclude(r => r.User)
                .Where(pm => pm.ProjectId == projectId)
                .ToListAsync();
        }

    }
}
