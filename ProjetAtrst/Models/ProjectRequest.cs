namespace ProjetAtrst.Models
{
    // ✅ Enums
    public enum RequestType
    {
        Join,       // Join request from researcher to project
        Invitation  // Invitation from project leader to researcher
    }

    public enum RequestStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    // ✅ Unified ProjectRequest entity
    public class ProjectRequest
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }


        // Sender
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        // Receiver
        public string ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }

        public string Message { get; set; } // Message attached to request or invitation

        public string? RelatedWorks { get; set; }
        public RequestType Type { get; set; } // Request type (join or invitation)
        public RequestStatus Status { get; set; } = RequestStatus.Pending;
        public DateOnly CreatedAt { get; set; } 


    }

}
