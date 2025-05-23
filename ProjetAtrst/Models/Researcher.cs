namespace ProjetAtrst.Models
{
    public class Researcher:ApplicationUser
    {
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
        public bool IsCompleted { get; set; } = false;
        public string? ApprovedByAdminId { get; set; } 
        public Admin? ApprovedByAdmin { get; set; }
        public bool? IsApprovedByAdmin { get; set; }
        // null=> pending, true=> approved, false=> rejected
       
    }
}
 