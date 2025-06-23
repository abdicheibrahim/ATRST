using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Models;

namespace ProjetAtrst.Repositories
{
    public class ProjectRepository :GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context) { }
        
        //Verified
        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Not Verified

        public async Task<List<Project>> GetAvailableProjectsForJoinAsync(string researcherId)
        {
            var joinedProjectIds = await _context.ProjectMemberships
                .Where(pm => pm.ResearcherId == researcherId)
                .Select(pm => pm.ProjectId)
                .ToListAsync();

            var requestedProjectIds = await _context.ProjectRequests
                .Where(j => j.SenderId == researcherId)
                .Select(j => j.ProjectId)
                .ToListAsync();

            return await _context.Projects
                .Where(p =>
                    p.ProjectStatus == ProjectStatus.Open &&
                    p.IsAcceptingJoinRequests &&
                    !joinedProjectIds.Contains(p.Id) &&
                    !requestedProjectIds.Contains(p.Id)
                )
                .Include(p => p.ProjectMemberships)
                    .ThenInclude(pm => pm.Researcher)
                        .ThenInclude(r => r.User)
                .ToListAsync();
        }

     


    }
}
