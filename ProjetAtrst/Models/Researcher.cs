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

        [MaxLength(255)]
        public string? Establishment { get; set; }

        public string? Statut { get; set; } // Position (e.g. lurker, permanent, etc.
        public string? Grade { get; set; }  //  Rank
        public string? Speciality { get; set; }

        public string? Diploma { get; set; }
        public string? DipInstitution { get; set; }
        public DateTime DipDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        // Would you like to contribute as a partner
        public bool WantsToContributeAsPartner { get; set; } = false;

        //  Economic contributions (if any)
        public string? SocioEconomicContributions { get; set; }

        // Administrative approval
        public ResearcherApprovalStatus ResearcherApprovalStatus { get; set; } = ResearcherApprovalStatus.Pending;

        // Relationships
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<ProjectMembership>? ProjectMemberships { get; set; }
        public ICollection<ProjectRequest>? ProjectRequests { get; set; }

        public string? ApprovedByAdminId { get; set; }
        public Admin? ApprovedByAdmin { get; set; }
    }

}
