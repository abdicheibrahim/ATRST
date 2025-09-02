namespace ProjetAtrst.ViewModels.Partner
{
    public class PartnerProfileViewModel
    {
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
        [DataType(DataType.MultilineText)]
        public string? PartnerResearchPrograms { get; set; }

        [Display(Name = "Travaux d’intérêt socio-économique")]
        [DataType(DataType.MultilineText)]
        public string? PartnerSocioEconomicWorks { get; set; }
    }
}
