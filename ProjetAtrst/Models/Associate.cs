using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetAtrst.Models
{
    public class Associate
    {
        [Key]
        [Required]
        public string Id { get; set; } = default!;
        [ForeignKey(nameof(Id))]
        [Required]
        public ApplicationUser User { get; set; } = default!;


       
        public string? Diploma { get; set; }
        public string? Speciality { get; set; }
        public string? MemberParticipation { get; set; }

        // Administrative approval
        public ApprovalStatus PartenaireApprovalStatus { get; set; } = ApprovalStatus.Pending;
        public string? ApprovedByAdminId { get; set; }
        public Admin? ApprovedByAdmin { get; set; }
    }
}
