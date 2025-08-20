
namespace ProjetAtrst.Models
{
    public enum ProjectApprovalStatus
    {
        Pending,
        Accepted,
        Rejected
    }
    public enum ProjectStatus
    {
        Open,
        Closed,
        Archived
    }

    public class Project
    {

        

            [MaxLength(500)]
            public string Description { get; set; } = string.Empty;
           public bool IsAcceptingJoinRequests { get; set; } = true;

           
           

        

        [Key]
        public int Id { get; set; }

        // === 1. Informations de base ===
        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Keywords { get; set; }

        public string? Domain { get; set; }
        public string? Axis { get; set; }
        public string? Theme { get; set; }
        public string? Nature { get; set; }
        public string? PNR { get; set; }
        public string? TRL { get; set; }

        [Display(Name = "Durée du projet (en mois)")]
        public int DurationInMonths { get; set; } = 36;

        public string? HostInstitution { get; set; }

        // === 2. Présentation du projet ===
        public string? CurrentState { get; set; }    // Etat des lieux
        public string? Motivation { get; set; }
        public string? Methodology { get; set; }

        // === 3. Résultats attendus & Impact ===
        public string? SocioEconomicPartner { get; set; }
        public string? ExpectedResults { get; set; }
        public string? TargetSectors { get; set; }
        public string? Impact { get; set; }

        // === 4. Références ===
        public string? ReferencesJson { get; set; }  // Liste des références en JSON

        // === 5. Fichiers ou logo ===
        public string? LogoPath { get; set; }

        // === 6. État administratif du projet ===
        public bool IsCompleted { get; set; } = false;
        public ProjectApprovalStatus ProjectApprovalStatus { get; set; } = ProjectApprovalStatus.Pending;
        public ProjectStatus ProjectStatus { get; set; } = ProjectStatus.Open;

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime LastActivity { get; set; } = DateTime.UtcNow;

        public string? ApprovedByAdminId { get; set; }
        public Admin? ApprovedByAdmin { get; set; }

        // === 7. Relations ===
        public ICollection<ProjectMembership> ProjectMemberships { get; set; } = new List<ProjectMembership>();
        public ICollection<ProjectRequest>? ProjectRequests { get; set; }
    }
}
