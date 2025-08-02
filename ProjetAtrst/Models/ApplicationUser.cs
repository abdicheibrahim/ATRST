using Microsoft.AspNetCore.Identity;
namespace ProjetAtrst.Models

{
    public class ApplicationUser: IdentityUser

    {
        public Researcher? Researcher { get; set; }
        public Admin? Admin { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? LastName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? FirstNameAr { get; set; } = default!;
        [MaxLength(50)]
        public string? LastNameAr { get; set; } = default!;

        public string? FullName { get; set; }
        public string? Gender { get; set; } = default!;
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
        public DateTime Birthday { get; set; }
        public string? Mobile { get; set; }
        public string? ProfilePicturePath { get; set; }

    }
}
