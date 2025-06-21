namespace ProjetAtrst.ViewModels.Project
{
    public class ProjectEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "العنوان مطلوب")]
        [StringLength(100)]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "الوصف مطلوب")]
        public string Description { get; set; } = "";
    }
}
