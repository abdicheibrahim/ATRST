using Microsoft.AspNetCore.Identity;
namespace ProjetAtrst.Models
{
    public enum RoleType
    {
        Researcher, //  Chercheur
        Partner, //  Partenaire
        Associate //  Associe
    }
    public class ApplicationUser: IdentityUser
    {
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
        public DateOnly RegisterDate { get; set; } 
        public DateOnly Birthday { get; set; }
        public string? Mobile { get; set; }
        public string? ProfilePicturePath { get; set; }
        public bool IsCompleted { get; set; } = false;
        // Relationships
        public Researcher? Researcher { get; set; }
        public Partner? Partner { get; set; }
        public Associate? Associate { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<ProjectMembership>? ProjectMemberships { get; set; }
        public ICollection<ProjectRequest> SentRequests { get; set; }
        public ICollection<ProjectRequest> ReceivedRequests { get; set; }
        public Admin? Admin { get; set; }
        public RoleType RoleType { get; set; }

    }
}
