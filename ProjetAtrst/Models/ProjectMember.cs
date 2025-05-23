namespace ProjetAtrst.Models
{
    public class ProjectMember : Researcher
    {
        public int JoinedProjectId { get; set; }
        public Project? JoinedProject { get; set; }
        public ICollection<InvitationRequest>? ReceivedInvitations { get; set; } 
        public ICollection<JoinRequest>? SentJoinRequests { get; set; }
    }

}
