using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetAtrst.Models
{
    public enum ResearcherApprovalStatus
    {
        Pending,
        Accepted,
        Rejected
    }
 
    public class Researcher
    {
        [Key]
        [Required]
        public string Id { get; set; } = default!;

        [ForeignKey(nameof(Id))]
        [Required]
        public ApplicationUser User { get; set; } = default!;


        public string? Diploma { get; set; }
        public string? Grade { get; set; }
        public string? Speciality { get; set; }
        public string? Establishment { get; set; }
        public List<string>? ParticipationPrograms { get; set; } = new List<string>();
       

        // Administrative approval
        public ResearcherApprovalStatus ResearcherApprovalStatus { get; set; } = ResearcherApprovalStatus.Pending;
        public string? ApprovedByAdminId { get; set; }
        public Admin? ApprovedByAdmin { get; set; }
        public bool IsCompleted { get; set; } = false;
        // Relationships
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<ProjectMembership>? ProjectMemberships { get; set; }
        public ICollection<ProjectRequest>? ProjectRequests { get; set; }

    }

}
