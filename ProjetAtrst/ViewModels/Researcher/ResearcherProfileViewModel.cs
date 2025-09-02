namespace ProjetAtrst.ViewModels.Researcher
{
    public class ResearcherProfileViewModel
    {
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
