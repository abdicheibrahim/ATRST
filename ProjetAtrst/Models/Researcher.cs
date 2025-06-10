using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetAtrst.Models
{
    public class Researcher
    {

        [Key]
        public string Id { get; set; }

        [ForeignKey(nameof(Id))]
        public ApplicationUser User { get; set; }
        [MaxLength(50)]
        public string? Establishment { get; set; }
        public string? Status { get; set; }
        public string? Grade { get; set; }
        public string? Speciality { get; set; }
        public string? Mobile { get; set; }
        public string? Diploma { get; set; }
        public string? DipInstitution { get; set; }
        public DateTime DipDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string? ApprovedByAdminId { get; set; } 
        public Admin? ApprovedByAdmin { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        public Expert? Expert { get; set; }
        public ProjectMember? ProjectMember { get; set; }
        public ProjectLeader? ProjectLeader { get; set; }
       
    }
}
 