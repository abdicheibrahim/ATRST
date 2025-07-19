using NuGet.Protocol.Plugins;
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

        public async Task<(List<Project> Projects, int TotalCount)> GetAvailableProjectsForJoinAsync(string researcherId, int pageNumber, int pageSize)
        {
            var joinedProjectIds = await _context.ProjectMemberships
                .Where(pm => pm.ResearcherId == researcherId)
                .Select(pm => pm.ProjectId)
                .ToListAsync();

            var requestedProjectIds = await _context.ProjectRequests
                .Where(j => j.SenderId == researcherId)
                .Select(j => j.ProjectId)
                .ToListAsync();

            var query = _context.Projects
                .Where(p =>
                    p.ProjectStatus == ProjectStatus.Open &&
                    p.IsAcceptingJoinRequests &&
                    !joinedProjectIds.Contains(p.Id) &&
                    !requestedProjectIds.Contains(p.Id)
                )
                .Include(p => p.ProjectMemberships)
                    .ThenInclude(pm => pm.Researcher)
                        .ThenInclude(r => r.User);

            var totalCount = await query.CountAsync();

            var projects = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (projects, totalCount);
        }



        public async Task<(string ProjectTitle, string LeaderFullName)> GetProjectInfoAsync(int projectId)
        {
             var result = await _context.ProjectMemberships
            .Include(pm => pm.Project)
            .Include(pm => pm.Researcher)
            .ThenInclude(r => r.User)
            .Where(pm => pm.ProjectId == projectId && pm.Role== Role.Leader)
            .Select(pm => new
            {
                ProjectTitle = pm.Project.Title,
                LeaderFullName = pm.Researcher.User.FullName 
            })
            .FirstOrDefaultAsync();


            if (result == null)
                return (null, null);

            return (result.ProjectTitle, result.LeaderFullName);
        }



    }
}
