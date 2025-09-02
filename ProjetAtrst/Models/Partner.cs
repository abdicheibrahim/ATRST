using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetAtrst.Models
{
    public class Partner
    {
        [Key]
        [Required]
        public string Id { get; set; } = default!;
        [ForeignKey(nameof(Id))]
        [Required]
        public ApplicationUser User { get; set; } = default!;


        public string? Baccalaureat { get; set; }
        public string? Diploma { get; set; }
        public string? Profession { get; set; }
        public string? Speciality { get; set; }
        public string Establishment { get; set; } = string.Empty;
        public string? PartnerResearchPrograms { get; set; }
        public string? PartnerSocioEconomicWorks { get; set; }

        // Administrative approval
        public ApprovalStatus PartenaireApprovalStatus { get; set; } = ApprovalStatus.Pending;
        public string? ApprovedByAdminId { get; set; }
        public Admin? ApprovedByAdmin { get; set; }

        // Relationships
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<ProjectMembership>? ProjectMemberships { get; set; }
        public ICollection<ProjectRequest>? ProjectRequests { get; set; }
    }
}
