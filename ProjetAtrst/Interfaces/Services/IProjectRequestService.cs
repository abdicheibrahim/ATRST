using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.ProjectRequests;

namespace ProjetAtrst.Interfaces.Services
{
    public interface IProjectRequestService
    {
        Task SendRequestAsync(ProjectRequestCreateViewModel model, string senderId);
        Task AcceptRequestAsync(int requestId);
        Task RejectRequestAsync(int requestId);
        Task<(IEnumerable<ProjectRequest> Incoming, IEnumerable<ProjectRequest> Sent)> GetRequestsForDashboardAsync(string userId);

        Task<IEnumerable<ProjectRequest>> GetOutgoingRequestsAsync(string userId);
    }
}
