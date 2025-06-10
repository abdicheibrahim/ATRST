namespace ProjetAtrst.Models
{
    public enum InvitationRequestStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public class InvitationRequest
    {
        public int Id { get; set; }
        public int TargetProjectId { get; set; }
        public Project TargetProject { get; set; } = default!;

        public string SenderId { get; set; } = default!;
        public ProjectLeader Sender { get; set; } = default!;

        public string ReceiverId { get; set; } = default!;
        public ProjectMember Receiver { get; set; } = default!;

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public InvitationRequestStatus Status { get; set; } =InvitationRequestStatus.Pending;
    }
}
