using ProjetAtrst.Models;

namespace ProjetAtrst.ViewModels.ProjectRequests
{
    public class JoinRequestISentViewModel
    {
        public int RequestId { get; set; }

        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; } = string.Empty;

        public DateTime RequestedAt { get; set; }

        public JoinRequestStatus Status { get; set; }
    }

}
