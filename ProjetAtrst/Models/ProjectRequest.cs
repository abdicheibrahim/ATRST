namespace ProjetAtrst.Models
{
    // ✅ Enums
    public enum RequestType
    {
        Join,       // طلب انضمام من باحث إلى مشروع
        Invitation  // دعوة من قائد المشروع إلى باحث
    }

    public enum RequestStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    // ✅ الكيان الموحد ProjectRequest
    public class ProjectRequest
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }


        // المرسل
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        // المستقبِل
        public string ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }

        public string Message { get; set; } // رسالة مرفقة بالطلب أو الدعوة
        public RequestType Type { get; set; } // نوع الطلب (انضمام أم دعوة)
        public RequestStatus Status { get; set; } = RequestStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        //public string SenderId { get; set; } // المستخدم الذي أرسل الطلب (باحث أو قائد)
        //public Researcher Sender { get; set; }

        //public string ReceiverId { get; set; } // المستخدم المستهدف بالطلب (قائد أو باحث)
        //public Researcher Receiver { get; set; }

    }

}
