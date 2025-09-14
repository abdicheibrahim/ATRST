namespace ProjetAtrst.ViewModels.Project
{
    public class ProjectEditViewModel
    {
        public int Id { get; set; }
        // ✅ To receive new image
        public IFormFile? LogoFile { get; set; }
        // ✅ To display current image (optional)
        public string? CurrentLogoPath { get; set; } = "~/images/default-project.png";

        [Required(ErrorMessage = "Le titre du projet est obligatoire.")]
        [StringLength(200, ErrorMessage = "Le titre ne doit pas dépasser 200 caractères.")]
        [Display(Name = "Titre du projet")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Le PNR est obligatoire.")]
        [Display(Name = "Programmes Nationaux de Recherche")]
        public string PNR { get; set; }

        [Required(ErrorMessage = "La nature du projet est obligatoire.1")]
        [Display(Name = "Nature du projet")]
        public string Nature { get; set; }

        [Required(ErrorMessage = "Le domaine est obligatoire.")]
        [Display(Name = "Domaine")]
        public string Domain { get; set; }

        [Display(Name = "Axe")]
        public string Axis { get; set; }

        [Display(Name = "Thème")]
        public string Theme { get; set; }

        [Display(Name = "Mots-clés")]
        public List<string> Keywords { get; set; } = new List<string>();


        [Required(ErrorMessage = "L'Établissement est obligatoire.")]
        [Display(Name = "Établissement d'accueil")]
        public string HostInstitution { get; set; }

        [Display(Name = "Niveau TRL")]
        public string TRL { get; set; }

        [Required(ErrorMessage = "La durée est obligatoire.")]
        [Range(1, 60, ErrorMessage = "La durée doit être entre 1 et 60 mois.")]
        [Display(Name = "Durée (mois)")]
        public int DurationInMonths { get; set; }

        [Display(Name = "Autoriser les chercheurs à demander à rejoindre ce projet")]
        public bool IsAcceptingJoinRequests { get; set; }

        [Display(Name = "État des lieux")]
        [StringLength(2000, ErrorMessage = "L'état des lieux ne doit pas dépasser 2000 caractères.")]
        public string CurrentState { get; set; }

        [Display(Name = "Motivations du projet")]
        [StringLength(2000, ErrorMessage = "Les motivations ne doivent pas dépasser 2000 caractères.")]
        public string Motivation { get; set; }

        [Display(Name = "Méthodologie")]
        [StringLength(3000, ErrorMessage = "La méthodologie ne doit pas dépasser 3000 caractères.")]
        public string Methodology { get; set; }

        [Display(Name = "Partenaire socio-économique")]
        public string SocioEconomicPartner { get; set; }

        [Display(Name = "Secteurs cibles")]
        public string TargetSectors { get; set; }

        [Display(Name = "Résultats attendus")]
        [StringLength(2000, ErrorMessage = "Les résultats attendus ne doivent pas dépasser 2000 caractères.")]
        public string ExpectedResults { get; set; }

        [Display(Name = "Impact socio-économique")]
        [StringLength(2000, ErrorMessage = "L'impact ne doit pas dépasser 2000 caractères.")]
        public string Impact { get; set; }

        [Display(Name = "Références bibliographiques")]
        public string References { get; set; }
    }
}
