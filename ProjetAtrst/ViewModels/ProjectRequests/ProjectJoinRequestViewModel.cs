using ProjetAtrst.Models;

namespace ProjetAtrst.ViewModels.ProjectRequests
{
    public class ProjectJoinRequestViewModel
    {
        public int RequestId { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public RoleType SenderRole { get; set; }    
        public RequestStatus Status { get; set; }
        public DateOnly SentAt { get; set; }
    }

}
