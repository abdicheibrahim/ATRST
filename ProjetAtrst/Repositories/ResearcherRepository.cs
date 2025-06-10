using ProjetAtrst.Models;
using ProjetAtrst.Interfaces.Repositories;

namespace ProjetAtrst.Repositories
{
    public class ResearcherRepository : GenericRepository<Researcher>, IResearcherRepository
    {
        public ResearcherRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Researcher?> GetByUserIdAsync(string userId)
        {
            return await _context.Researchers
                .Include(r => r.ProjectLeader)
                .Include(r => r.ProjectMember)
                .FirstOrDefaultAsync(r => r.Id == userId);
        }
        public async Task<bool> CanCreateProjectAsync(string researcherId)
        {
            var researcher = await GetByUserIdAsync(researcherId);
            return researcher != null && researcher.ProjectLeader == null && researcher.ProjectMember == null;
        }
       

    }

}

