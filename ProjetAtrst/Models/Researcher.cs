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
        public string Id { get; set; }=default!;

        [ForeignKey(nameof(Id))]
        [Required]
        public ApplicationUser User { get; set; }=default!;
        [MaxLength(255)]
        public string? Establishment { get; set; }
        public ResearcherApprovalStatus ResearcherApprovalStatus { get; set; } = ResearcherApprovalStatus.Pending;
        public string? Grade { get; set; }
        public string? Speciality { get; set; }
        public string? Mobile { get; set; }
        public string? Diploma { get; set; }
        public string? DipInstitution { get; set; }
        public DateTime DipDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public bool IsApprovedByAdmin { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<ProjectMembership>? ProjectMemberships { get; set; }
        public ICollection<ProjectRequest>? ProjectRequests { get; set; }

        //future: add a property for the Admin & Files of the researcher
        public string? ApprovedByAdminId { get; set; } 
        public Admin? ApprovedByAdmin { get; set; }
       
        
    }
}
 