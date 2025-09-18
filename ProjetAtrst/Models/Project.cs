
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

        // === 1. Basic Information ===
        [Required]
        public string Title { get; set; } = string.Empty;

        public List<string> Keywords { get; set; } = new List<string>();

        public string? Domain { get; set; }
        public string? Axis { get; set; }
        public string? Theme { get; set; }
        public string? Nature { get; set; }
        public string? PNR { get; set; }
        public string? TRL { get; set; }

        [Display(Name = "Durée du projet (en mois)")]
        public int DurationInMonths { get; set; } = 36;

        public string? HostInstitution { get; set; }

        // === 2. Project Presentation ===
        public string? CurrentState { get; set; }    
        public string? Motivation { get; set; }
        public string? Methodology { get; set; }

        // === 3. Expected Results & Impact ===
        public string? SocioEconomicPartner { get; set; }
        public string? ExpectedResults { get; set; }
        public string? TargetSectors { get; set; }
        public string? Impact { get; set; }

        // === 4. References ===
        public string? ReferencesJson { get; set; }  // List of references in JSON

        // === 5. Files or logo ===
        public string? LogoPath { get; set; }

        // === 6. Project Administrative Status ===
        public bool IsCompleted { get; set; } = false;
        public ProjectApprovalStatus ProjectApprovalStatus { get; set; } = ProjectApprovalStatus.Pending;
        public ProjectStatus ProjectStatus { get; set; } = ProjectStatus.Open;

        public DateOnly CreationDate { get; set; } 
        public DateOnly LastActivity { get; set; } 

        public string? ApprovedByAdminId { get; set; }
        public Admin? ApprovedByAdmin { get; set; }

        // === 7. Relationships ===
        public ICollection<ProjectMembership> ProjectMemberships { get; set; } = new List<ProjectMembership>();
        public ICollection<ProjectRequest>? ProjectRequests { get; set; }
        public ICollection<ProjectTask>? Tasks { get; set; }
    }
}
