using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjetAtrst.ViewModels.Researcher
{
    public class EditResearcherProfileViewModel
    {

        

        [Required(ErrorMessage = "Establishment is required")]
        [Display(Name = "Establishment")]
        public string Establishment { get; set; } = string.Empty;

        public List<SelectListItem> EstablishmentsList { get; set; } = new();

        [Required(ErrorMessage = "Grade is required")]
        [Display(Name = "Grade")]
        public string Grade { get; set; } = string.Empty;

        [Required(ErrorMessage = "Speciality is required")]
        [Display(Name = "Speciality")]
        public string Speciality { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number format")]
        [Display(Name = "Mobile")]
        public string? Mobile { get; set; }

        [Required(ErrorMessage = "Diploma is required")]
        [Display(Name = "Diploma")]
        public string Diploma { get; set; } = string.Empty;

        [Required(ErrorMessage = "Diploma Institution is required")]
        [Display(Name = "Diploma Institution")]
        public string DipInstitution { get; set; } = string.Empty;

        [Required(ErrorMessage = "Diploma Date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Diploma Date")]
        public DateTime DipDate { get; set; }
        public bool IsLeader { get; set; } = false;
        public bool IsMember { get; set; } = false;
        // Read-only properties for display
        public bool IsCompleted { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
    }
}
