
namespace ProjetAtrst.Models
{
    public enum ProjectApprovalStatus
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

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime LastActivity { get; set; } = DateTime.UtcNow;
        public bool IsCompleted { get; set; } = false;
        public ProjectApprovalStatus ProjectApprovalStatus { get; set; } = ProjectApprovalStatus.Pending;
        public ProjectStatus ProjectStatus { get; set; }
        public bool IsAcceptingJoinRequests { get; set; } = true;

        public ICollection<ProjectMembership> ProjectMemberships { get; set; } = new List<ProjectMembership>();
        public ICollection<InvitationRequest>? SentInvitations { get; set; }
        public ICollection<JoinRequest>? JoinRequests { get; set; }
        public string? ApprovedByAdminId { get; set; }
        public Admin? ApprovedByAdmin { get; set; } = default!;

    }
}
