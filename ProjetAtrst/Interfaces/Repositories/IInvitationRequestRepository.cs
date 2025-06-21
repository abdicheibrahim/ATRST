using ProjetAtrst.Models;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IInvitationRequestRepository
    {
        Task<List<InvitationRequest>> GetInvitationsISentAsync(string leaderId);
        Task<List<InvitationRequest>> GetInvitationsIReceivedAsync(string researcherId);
    }
}
