using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjetAtrst.ViewModels.Researcher
{
    public class EditResearcherProfileViewModel
    {
      

        [Required(ErrorMessage = "Establishment is required")]
        [Display(Name = "Establishment")]
        public string Establishment { get; set; } = string.Empty;
       

        [Required(ErrorMessage = "Grade is required")]
        [Display(Name = "Grade")]
        public string Grade { get; set; } = string.Empty;
       

        [Required(ErrorMessage = "Speciality is required")]
        [Display(Name = "Speciality")]
        public string Speciality { get; set; } = string.Empty;


        [Required(ErrorMessage = "Diploma is required")]
        [Display(Name = "Diploma")]
        public string Diploma { get; set; } = string.Empty;

       
        [Display(Name = "Participation à des programmes de recherche")]
        public List<string>? ParticipationPrograms { get; set; }

        // Read-only properties for display
        public bool IsCompleted { get; set; }
        public bool IsApprovedByAdmin { get; set; }
       
    }
}
