
namespace ProjetAtrst.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
       
        public string Title { get; set; }= string.Empty;

        public DateTime creationDate { get; set; } = DateTime.UtcNow;

        [MaxLength(500)]
        public string Description { get; set; }=string.Empty;
        public string LeaderId { get; set; } = default!;
        public ProjectLeader Leader { get; set; } = default!;
        public bool IsCompleted { get; set; } = false;
        public bool? IsApprovedByAdmin { get; set; } = false;
        // null=> pending, true=> approved, false=> rejected
        public string? ApprovedByAdminId { get; set; }
        public Admin? ApprovedByAdmin { get; set; } = default!;
        public ICollection<ProjectMember> Members { get; set; } = new List<ProjectMember>();
        public ICollection<JoinRequest> JoinRequests { get; set; } = new List<JoinRequest>();
        public ICollection<ProjectEvaluation> Evaluations { get; set; } = new List<ProjectEvaluation>();
        public ICollection<ProjectFile> Files { get; set; } = new List<ProjectFile>();
        public ICollection<InvitationRequest>? SentInvitations { get; set; }

    }
}
