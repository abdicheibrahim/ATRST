using ProjetAtrst.Models;

namespace ProjetAtrst.ViewModels.ProjectRequests
{
    public class InvitationIReceivedViewModel
    {
        public int InvitationId { get; set; }

        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; } = string.Empty;

        public string SenderLeaderId { get; set; }
        public string SenderLeaderFullName { get; set; } = string.Empty;

        public DateTime SentAt { get; set; }

        public InvitationRequestStatus Status { get; set; }
    }

}
