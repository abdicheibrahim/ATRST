
namespace ProjetAtrst.Models
{
    public enum Status
    {
        Pending,
        Accepted,
        Rejected
    }
    public enum ProjectStatus
    {
        Open,
        Closed,
        Archived
    }

    public class Project
    {
        [Key]
        public int Id { get; set; }
       
        public string Title { get; set; }= string.Empty;

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [MaxLength(500)]
        public string Description { get; set; }=string.Empty;
        public DateTime LastActivity { get; set; } = DateTime.UtcNow;

        public string LeaderId { get; set; } = default!;
        public ProjectLeader Leader { get; set; } = default!;
        public bool IsCompleted { get; set; } = false;
        public Status Status { get; set; } = Status.Pending;
        public ProjectStatus ProjectStatus { get; set; } = ProjectStatus.Open;
        public string? ApprovedByAdminId { get; set; }
        public Admin? ApprovedByAdmin { get; set; } = default!;
        public ICollection<ProjectMembership>? ProjectMemberships { get; set; }
        public ICollection<JoinRequest> JoinRequests { get; set; } = new List<JoinRequest>();
        public ICollection<ProjectEvaluation> Evaluations { get; set; } = new List<ProjectEvaluation>();
        public ICollection<ProjectFile> Files { get; set; } = new List<ProjectFile>();
        public ICollection<InvitationRequest>? SentInvitations { get; set; }

    }
}
