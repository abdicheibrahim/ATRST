namespace ProjetAtrst.Models
{
    public enum NotificationType
    {
        JoinRequestSent,       // تم إرسال طلب انضمام لقائد المشروع
        JoinRequestAccepted,   // تم قبول طلب الانضمام
        JoinRequestRejected,   // تم رفض طلب الانضمام
        InvitationReceived,    // تلقى دعوة للانضمام
        InvitationAccepted,    // تم قبول الدعوة
        InvitationRejected,    // تم رفض الدعوة
        General                // إشعار عام
    }
    public class Notification
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public Researcher User { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }

        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public NotificationType Type { get; set; } = NotificationType.General;

        // (اختياري) يمكنك إضافة حقل لربط الإشعار بكائن معين مثل ProjectId أو RequestId
        public int? RelatedEntityId { get; set; }
    }

}
