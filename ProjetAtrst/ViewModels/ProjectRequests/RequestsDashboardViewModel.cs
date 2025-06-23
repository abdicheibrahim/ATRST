using ProjetAtrst.Models;

namespace ProjetAtrst.ViewModels.ProjectRequests
{
    public class RequestsDashboardViewModel
    {
        public IEnumerable<ProjectRequest> IncomingRequests { get; set; } = Enumerable.Empty<ProjectRequest>();
        public IEnumerable<ProjectRequest> SentRequests { get; set; } = Enumerable.Empty<ProjectRequest>();
    }

}
