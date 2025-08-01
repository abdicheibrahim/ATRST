using ProjetAtrst.Enums;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.ProjectRequests;

namespace ProjetAtrst.Interfaces.Services
{
    public interface IProjectRequestService
    {
        Task SendRequestAsync(ProjectRequestCreateViewModel model, string senderId);
        Task AcceptRequestAsync(int requestId);
        //Task RejectRequestAsync(int requestId);
        Task RejectRequestAsync(int requestId, RejectionType rejectionType);
        Task<(IEnumerable<ProjectRequest> Incoming, IEnumerable<ProjectRequest> Sent)> GetRequestsForDashboardAsync(string userId);
        Task<ProjectRequest> GetByIdWithRelationsAsync(int userId);
        Task<IEnumerable<ProjectRequest>> GetOutgoingRequestsAsync(string userId);
        Task<ProjectRequestCreateViewModel> PrepareRequestModelAsync(int projectId, string receiverId, RequestType type);

        Task<List<ProjectRequest>> GetSentJoinRequestsAsync(string researcherId);
        //Task<List<ProjectRequest>> GetSentInvitationsAsync(string leaderId);
        Task<List<ProjectRequest>> GetMyInvitationsAsync(string userId);
        Task SendInvitationAsync(int projectId, string researcherId);

    }
}
