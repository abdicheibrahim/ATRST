namespace ProjetAtrst.Models
{
    public class Researcher:ApplicationUser
    {
        [MaxLength(50)]
        public string FirstNameAr { get; set; } = default!;
        [MaxLength(50)]
        public string LastNameAr { get; set; } =default!;
        public string Gender { get; set; } = default!;
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
        public DateTime Birthday { get; set; }
        public string Establishment { get; set; }
        public string Status { get; set; }
        public string Grade { get; set; }
        public string Speciality { get; set; }
        public string? Mobile { get; set; }
        public string Diploma { get; set; }
        public string DipInstitution { get; set; }
        public DateTime DipDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string? ApprovedByAdminId { get; set; } 
        public Admin? ApprovedByAdmin { get; set; }
        public bool? IsApprovedByAdmin { get; set; }
        // null=> pending, true=> approved, false=> rejected
       
    }
}
 