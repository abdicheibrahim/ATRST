using ProjetAtrst.Models;

namespace ProjetAtrst.ViewModels.ProjectRequests
{
    public class ProjectRequestCreateViewModel
    {
        public int ProjectId { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "رسالتك")]
        public string Message { get; set; }

        [Required] // المستخدم المستقبل
        public string ReceiverId { get; set; }

        public RequestType Type { get; set; } // Join أو Invitation

        public string? ProjectTitle { get; set; }
        public string? LeaderFullName { get; set; }
    }
}

