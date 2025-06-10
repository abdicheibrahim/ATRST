using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Models;

namespace ProjetAtrst.Repositories
{
    public class JoinRequestRepository : GenericRepository<Researcher>, IJoinRequestRepository
    {
        public JoinRequestRepository(ApplicationDbContext context) : base(context) { }
        public async Task<int> CountPendingForLeaderAsync(string leaderId)
        {
            return await _context.JoinRequests
                .CountAsync(r => r.Project.LeaderId == leaderId && r.Status == JoinRequestStatus.Pending);
        }

    }
}
