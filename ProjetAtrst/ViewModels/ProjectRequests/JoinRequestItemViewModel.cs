using ProjetAtrst.Models;

namespace ProjetAtrst.ViewModels.ProjectRequests
{
    public class JoinRequestItemViewModel
    {
        public int RequestId { get; set; }

        public string RequesterId { get; set; }
        public string RequesterFullName { get; set; }

        public DateTime RequestedAt { get; set; }
        public JoinRequestStatus Status { get; set; }
    }

}
