namespace ProjetAtrst.Models
{
    public enum NotificationType
    {
        JoinRequestSent,       // Join request sent to project leader
        JoinRequestAccepted,   // Join request accepted
        JoinRequestRejected,   // Join request rejected
        InvitationReceived,    // Invitation received to join
        InvitationAccepted,    // Invitation accepted
        InvitationRejected,    // Invitation rejected
        General                // General notification
    }
    public class Notification
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }

        public bool IsRead { get; set; } = false;
        public DateOnly CreatedAt { get; set; }

        public NotificationType Type { get; set; } = NotificationType.General;

        // (Optional) You can add a field to link the notification to a specific entity like ProjectId or RequestId
        public int? RelatedEntityId { get; set; }
    }

}
