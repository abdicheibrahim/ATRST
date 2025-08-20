

using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjetAtrst.ViewModels.Project
{
    public class ProjectCreateViewModel
    {
        
        public bool IsAcceptingJoinRequests { get; set; } = true;
        // === 1. Informations de base ===
        [Required(ErrorMessage = "Le titre est requis")]
        [Display(Name = "Titre du projet")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Mots-clés")]
        public string? Keywords { get; set; }

        [Display(Name = "Domaine")]
        public string? Domain { get; set; }

        [Display(Name = "Axe")]
        public string? Axis { get; set; }

        [Display(Name = "Thème")]
        public string? Theme { get; set; }
        [Display(Name = "Programme national de recherche")]
        public string? PNR { get; set; }

        [Display(Name = "Nature du projet")]
        public string? Nature { get; set; }

        [Display(Name = "Niveau TRL")]
        public string? TRL { get; set; }

        [Display(Name = "Durée (en mois)")]
        [Range(1, 60, ErrorMessage = "La durée doit être comprise entre 1 et 60 mois")]
        public int DurationInMonths { get; set; } = 36;

        [Display(Name = "Établissement d’accueil")]
        public string? HostInstitution { get; set; }

        // === 2. Présentation du projet ===
        [Display(Name = "État des lieux")]
        public string? CurrentState { get; set; }

        [Display(Name = "Motivations du projet")]
        public string? Motivation { get; set; }

        [Display(Name = "Méthodologie")]
        public string? Methodology { get; set; }

        // === 3. Résultats attendus et impact ===
        [Display(Name = "Partenaire socio-économique")]
        public string? SocioEconomicPartner { get; set; }

        [Display(Name = "Résultats attendus")]
        public string? ExpectedResults { get; set; }

        [Display(Name = "Secteurs cibles")]
        public string? TargetSectors { get; set; }

        [Display(Name = "Impact socio-économique")]
        public string? Impact { get; set; }

        // === 4. Références ===
        [Display(Name = "Références bibliographiques (liste séparée par des lignes)")]
        public List<string>? References { get; set; } 

        // === 5. Fichier du logo ===
        //[Display(Name = "Logo du projet (facultatif)")]
        //public IFormFile? LogoFile { get; set; }

        // === Dropdown Lists  ===
        public List<SelectListItem>? DomainsList { get; set; }
        public List<SelectListItem>? AxesList { get; set; }
        public List<SelectListItem>? ThemesList { get; set; }
        public List<SelectListItem>? NaturesList { get; set; }
        public List<SelectListItem>? TRLLevelsList { get; set; }
        public List<SelectListItem>? PNRList { get; set; }
    }
}
