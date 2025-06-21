//using ProjetAtrst.Models;
//using ProjetAtrst.Interfaces.Repositories;
//namespace ProjetAtrst.Repositories
//{
//    public class InvitationRequestRepository : IInvitationRequestRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public InvitationRequestRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<InvitationRequest>> GetInvitationsISentAsync(string leaderId)
//        {
//            var myProjectIds = await _context.ProjectMemberships
//                .Where(pm => pm.ResearcherId == leaderId && pm.Role == Role.Leader)
//                .Select(pm => pm.ProjectId)
//                .ToListAsync();

//            return await _context.InvitationRequests
//                .Include(i => i.Receiver)
//                    .ThenInclude(r => r.User)
//                .Include(i => i.TargetProject)
//                .Where(i => myProjectIds.Contains(i.TargetProjectId))
//                .ToListAsync();
//        }

//        public async Task<List<InvitationRequest>> GetInvitationsIReceivedAsync(string researcherId)
//        {
//            return await _context.InvitationRequests
//                .Include(i => i.TargetProject)
//                    .ThenInclude(p => p.ProjectMemberships)
//                        .ThenInclude(pm => pm.Researcher)
//                            .ThenInclude(r => r.User)
//                .Where(i => i.ReceiverId == researcherId)
//                .ToListAsync();
//        }
//    }
//}
