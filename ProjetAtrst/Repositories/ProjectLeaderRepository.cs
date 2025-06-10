using ProjetAtrst.Models;
using ProjetAtrst.Interfaces.Repositories;

namespace ProjetAtrst.Repositories
{
    public class ProjectLeaderRepository : GenericRepository<ProjectLeader>, IProjectLeaderRepository
    {
        public ProjectLeaderRepository(ApplicationDbContext context) : base(context) { }
       
        public async Task<ProjectLeader> GetProjectLeadersByResearcherIdAsync(string researcherId)
        {
            //return await _context.ProjectLeaders
            //    .Include(pl => pl.Researcher)
            //        .ThenInclude(r => r.User)
            //    .Where(pl => pl.Id == researcherId)
            //    .ToListAsync();

            return await _context.ProjectLeaders
                .Include(pl => pl.CreatedProject).FirstOrDefaultAsync(r => r.Id == researcherId);
        }
    }
}
