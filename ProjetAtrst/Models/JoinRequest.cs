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

        public string ResearcherId { get; set; }
        public Researcher Researcher { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public string Message { get; set; }

        public JoinRequestStatus Status { get; set; } = JoinRequestStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }


}
