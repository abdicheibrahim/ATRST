using ProjetAtrst.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetAtrst.Models
{
    public class ProjectMember 
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey(nameof(Id))]
        public Researcher Researcher { get; set; }
        public ICollection<ProjectMembership>? ProjectMemberships { get; set; }
        public ICollection<InvitationRequest>? ReceivedInvitations { get; set; } 
        public ICollection<JoinRequest>? SentJoinRequests { get; set; }
        
    }

}