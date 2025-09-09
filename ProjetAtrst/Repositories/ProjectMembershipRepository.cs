using Microsoft.EntityFrameworkCore;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Models;

namespace ProjetAtrst.Repositories
{
    public class ProjectMembershipRepository : GenericRepository<ProjectMembership>, IProjectMembershipRepository
    {
        public ProjectMembershipRepository (ApplicationDbContext context) : base(context) { }
        
        //Verified
        public async Task<IEnumerable<ProjectMembership>> GetAllByUserWithProjectsAsync(string userId)
        {
            return await _context.ProjectMemberships
                .Where(pm => pm.UserId == userId)
                .Include(pm => pm.Project)
                .ToListAsync();
        }
        public async Task<ProjectMembership?> GetByResearcherAndProjectAsync(string researcherId, int projectId)
        {
            return await _context.ProjectMemberships
                .Include(pm => pm.Project)
                .FirstOrDefaultAsync(pm => pm.UserId == researcherId && pm.ProjectId == projectId);
        }
        

        public async Task<List<ProjectMembership>> GetProjectsByUserWithDetailsAsync(string userId)
        {
            return await _context.ProjectMemberships
                .Where(pm => pm.UserId == userId)  // نستعمل UserId مباشرة
                .Include(pm => pm.Project)
                    .ThenInclude(p => p.ProjectMemberships)
                        .ThenInclude(m => m.User) // بدل Researcher → User
                .Include(pm => pm.Project.ProjectRequests)
                .ToListAsync();
        }

        public async Task<bool> IsUserLeaderAsync(string researcherId, int projectId)
        {
            return await _context.ProjectMemberships.AnyAsync(m =>
                m.ProjectId == projectId &&
                m.UserId == researcherId &&
                m.Role == Role.Leader);
        }

        public async Task<List<ProjectMembership>> GetMembersByProjectIdAsync(int projectId)
        {
            return await _context.ProjectMemberships
                .Include(pm => pm.User)
                .Where(pm => pm.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<int> CountProjectsByUserIdAsync(string userId)
        {
            return await _dbSet
                .Where(pm => pm.UserId == userId)
                .Select(pm => pm.ProjectId)   
                .Distinct()                  
                .CountAsync();
        }

    }
}
