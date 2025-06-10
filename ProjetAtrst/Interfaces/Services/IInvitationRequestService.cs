using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.Researcher;

namespace ProjetAtrst.Interfaces.Services
{
    public interface IInvitationRequestService
    {
        Task<List<InvitationRequest>> GetReceivedInvitationsAsync(string memberId);
        Task<List<InvitationRequest>> GetSentInvitationsAsync(string leaderId);
        Task<InvitationRequest?> GetByIdAsync(int id);
        Task<bool> AcceptAsync(int id);
        Task<bool> RejectAsync(int id);
        Task SendInvitationAsync(string senderId, string receiverId, int projectId);
        Task<List<ResearcherViewModel>> GetAllEligibleForInvitationAsync(int projectId);
        Task SendInvitationAsync(string receiverId, int projectId);


    }
}
