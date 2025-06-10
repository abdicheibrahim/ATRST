using ProjetAtrst.Models;

namespace ProjetAtrst.Interfaces.Repositories
{

    public interface IInvitationRequestRepository : IGenericRepository<InvitationRequest>
    {
        Task<List<InvitationRequest>> GetPendingReceivedAsync(string memberId);
        Task<List<InvitationRequest>> GetPendingSentAsync(string leaderId);
        Task<InvitationRequest?> GetByIdWithDetailsAsync(int id);

    }

}   
