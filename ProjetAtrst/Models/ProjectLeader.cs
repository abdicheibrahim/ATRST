using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetAtrst.Models
{
    public class ProjectLeader 
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey(nameof(Id))]
        public Researcher Researcher { get; set; }
        public ICollection<InvitationRequest>? SentInvitations { get; set; } 
        public ICollection<JoinRequest>? JoinRequests { get; set; }
        public ICollection<ProjectFile>? UploadedFiles { get; set; }
        public Project CreatedProject { get; set; }
    }

}
