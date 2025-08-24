namespace ProjetAtrst.ViewModels.Account
{
    public class CompleteProfileViewModel
    {
        // Role Type Selection
        [Required(ErrorMessage = "Veuillez sélectionner votre rôle")]
        [Display(Name = "Type de profil")]
        public RoleType RoleType { get; set; }  // Chercheur | Partenaire | Associe

        // Common background information
        [MaxLength(50)]
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(50)]
        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "الاسم مطلوب")]
        [MaxLength(50, ErrorMessage = "الاسم يجب أن لا يتجاوز 50 حرفًا")]
        [RegularExpression(@"^[\u0621-\u064A\s]+$", ErrorMessage = "الاسم يجب أن يحتوي على حروف عربية ومسافات فقط")]
        [Display(Name = "الاسم")]
        public string FirstNameAr { get; set; } = string.Empty;

        [Required(ErrorMessage = "اللقب مطلوب")]
        [MaxLength(50, ErrorMessage = "اللقب يجب أن لا يتجاوز 50 حرفًا")]
        [RegularExpression(@"^[\u0621-\u064A\s]+$", ErrorMessage = "اللقب يجب أن يحتوي على حروف عربية ومسافات فقط")]
        [Display(Name = "اللقب")]
        public string LastNameAr { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select your gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birthday is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime Birthday { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        [Display(Name = "Mobile")]
        public string? Mobile { get; set; }

        [Display(Name = "Establishment")]
        public string Establishment { get; set; } = string.Empty;

        // =======================================================
        // Chercheur
        // =======================================================
        [Display(Name = "Diploma")]
        public string? Diploma { get; set; }

        [Display(Name = "Grade")]
        public string? Grade { get; set; }

        [Display(Name = "Speciality")]
        public string? Speciality { get; set; }

        [Display(Name = "Participation à des programmes de recherche")]
        public List<string>? ParticipationPrograms { get; set; }

        //[Display(Name = "Travaux en relation avec le projet proposé")]
        //public string? RelatedWorks { get; set; }

        // =======================================================
        // Partenaire
        // =======================================================
        [Display(Name = "Baccalauréat")]
        public string? Baccalaureat { get; set; }

        [Display(Name = "Profession")]
        public string? Profession { get; set; }

        [Display(Name = "Contributions socio-économiques")]
        [DataType(DataType.MultilineText)]
        public string? SocioEconomicContributions { get; set; }

        [Display(Name = "Participation à des programmes de recherche (Partenaire)")]
        public string? PartnerResearchPrograms { get; set; }

        [Display(Name = "Travaux d’intérêt socio-économique")]
        public string? PartnerSocioEconomicWorks { get; set; }

        // =======================================================
        // associé
        // =======================================================
        [Display(Name = "Participation (Membre associé)")]
        public string? MemberParticipation { get; set; }

        // =======================================================
        // Account Status
        // =======================================================
        public bool IsLeader { get; set; } = false;
        public bool IsMember { get; set; } = false;

        public bool IsCompleted { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
    }
}
