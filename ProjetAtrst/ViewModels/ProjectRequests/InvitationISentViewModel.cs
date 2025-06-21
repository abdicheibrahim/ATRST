using ProjetAtrst.Models;

namespace ProjetAtrst.ViewModels.ProjectRequests
{
    public class InvitationISentViewModel
    {
        public int InvitationId { get; set; }

        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; } = string.Empty;

        public string ReceiverId { get; set; }
        public string ReceiverFullName { get; set; } = string.Empty;

        public DateTime SentAt { get; set; }

        public InvitationRequestStatus Status { get; set; }
    }

}
