using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Models;

namespace ProjetAtrst.Repositories
{
    public class InvitationRequestRepository : GenericRepository<InvitationRequest>, IInvitationRequestRepository
    {
        public InvitationRequestRepository(ApplicationDbContext context) : base(context) { }

        // للحصول على الطلبات المعلقة التي استلمها الباحث (كعضو)
        public async Task<List<InvitationRequest>> GetPendingReceivedAsync(string memberId)
        {
            return await _context.InvitationRequests
                .Include(r => r.TargetProject)
                    .ThenInclude(p => p.Leader)
                        .ThenInclude(l => l.Researcher)
                .Where(r => r.ReceiverId == memberId && r.Status == InvitationRequestStatus.Pending)
                .ToListAsync();
        }

        // للحصول على الطلبات المرسلة من قبل قائد المشروع
        public async Task<List<InvitationRequest>> GetPendingSentAsync(string leaderId)
        {
            return await _context.InvitationRequests
                .Include(r => r.Receiver)
                    .ThenInclude(m => m.Researcher)
                .Include(r => r.TargetProject)
                .Where(r => r.SenderId == leaderId && r.Status == InvitationRequestStatus.Pending)
                .ToListAsync();
        }

        public async Task<InvitationRequest?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.InvitationRequests
                .Include(r => r.TargetProject)
                    .ThenInclude(p => p.Leader)
                .Include(r => r.Receiver)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }

}
