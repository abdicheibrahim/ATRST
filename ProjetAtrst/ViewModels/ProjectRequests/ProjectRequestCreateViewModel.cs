using ProjetAtrst.Models;

namespace ProjetAtrst.ViewModels.ProjectRequests
{
    public class ProjectRequestCreateViewModel
    {
        public int ProjectId { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Your Message")]
        public string Message { get; set; }

        [Required] 
        public string ReceiverId { get; set; }

        public RequestType Type { get; set; } // Join or Invitation

        public string? ProjectTitle { get; set; }
        public string? LeaderFullName { get; set; }

        [Display(Name = "Travaux en relation avec le projet proposé")]
        public string? RelatedWorks { get; set; }
    }
}

