namespace ProjetAtrst.Models
{
    public class ProjectLeader : Researcher
    {
        public ICollection<InvitationRequest>? SentInvitations { get; set; } 
        public ICollection<JoinRequest>? JoinRequests { get; set; }
        public ICollection<ProjectFile>? UploadedFiles { get; set; }
        public Project? CreatedProject { get; set; }
    }

}
