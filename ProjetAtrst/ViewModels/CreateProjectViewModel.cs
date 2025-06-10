using ProjetAtrst.Models;

namespace ProjetAtrst.ViewModels

{
    public class CreateProjectViewModel
    {
        [Required]
        [Display(Name = "عنوان المشروع")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "وصف المشروع")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "حالة المشروع")]
        public ProjectStatus ProjectStatus { get; set; } 
    }
}
