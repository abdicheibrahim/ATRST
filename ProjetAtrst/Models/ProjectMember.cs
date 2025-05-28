namespace ProjetAtrst.Models
{
    public class ProjectMember : Researcher
    {
        public ICollection<ProjectMembership>? ProjectMemberships { get; set; }
        public ICollection<InvitationRequest>? ReceivedInvitations { get; set; } 
        public ICollection<JoinRequest>? SentJoinRequests { get; set; }
    }

}
