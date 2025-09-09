namespace ProjetAtrst.ViewModels.Associate
{
    public class AssociateDetailsViewModel
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

        [Display(Name = "Diploma")]
        public string? Diploma { get; set; }
        [Display(Name = "Speciality")]
        public string? Speciality { get; set; }
        [Display(Name = "Participation (Membre associé)")]
        public string? MemberParticipation { get; set; }
    }
}
