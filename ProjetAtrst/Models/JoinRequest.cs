namespace ProjetAtrst.Models
{
    public enum JoinRequestStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public class JoinRequest
    {
        public int Id { get; set; }

        public string RequesterId { get; set; } = default!;
        public ProjectMember Requester { get; set; } = default!;

        public int ProjectId { get; set; }
        public Project Project { get; set; } = default!;

        public DateTime RequestedAt { get; set; }
        public JoinRequestStatus Status { get; set; }
        
        public string? ApprovedById { get; set; }
        public ProjectLeader? ApprovedBy { get; set; }
    }

}
