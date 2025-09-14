namespace ProjetAtrst.ViewModels.Researcher
{
    public class ResearcherDetailsViewModel
    {

        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "الاسم واللقب")]
        public string FullNameAr { get; set; } = string.Empty;

        [Display(Name = "Gender")]
        public string Gender { get; set; } = string.Empty;

        [Display(Name = "Date de naissance")]
        public DateOnly Birthday { get; set; }

        [Display(Name = "Numéro de téléphone")]
        public string? Mobile { get; set; }

        [Display(Name = "Diploma")]
        public string? Diploma { get; set; }

        [Display(Name = "Grade")]
        public string? Grade { get; set; }

        [Display(Name = "Speciality")]
        public string? Speciality { get; set; }

        [Display(Name = "Establishment")]
        public string? Establishment { get; set; } = string.Empty;

        [Display(Name = "Participation à des programmes de recherche")]
        public List<string>? ParticipationPrograms { get; set; }

    }
}
