namespace ProjetAtrst.ViewModels.Partner
{
    public class PartnerDetailsViewModel
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "الاسم واللقب")]
        public string FullNameAr { get; set; } = string.Empty;

        [Display(Name = "Gender")]
        public string Gender { get; set; } = string.Empty;

        [Display(Name = "Date de naissance")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Numéro de téléphone")]
        public string? Mobile { get; set; }

        [Display(Name = "Baccalauréat")]
        public string? Baccalaureat { get; set; }

        [Display(Name = "Diploma")]
        public string? Diploma { get; set; }

        [Display(Name = "Profession")]
        public string? Profession { get; set; }

        [Display(Name = "Speciality")]
        public string? Speciality { get; set; }

        [Display(Name = "Establishment")]
        public string? Establishment { get; set; } = string.Empty;

        [Display(Name = "Participation à des programmes de recherche (Partenaire)")]
        public string? PartnerResearchPrograms { get; set; }

        [Display(Name = "Travaux d’intérêt socio-économique")]
        public string? PartnerSocioEconomicWorks { get; set; }
    }
}
