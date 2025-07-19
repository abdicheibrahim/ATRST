namespace ProjetAtrst.ViewModels.Project
{
    public class ProjectEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        // ✅ لإستقبال صورة جديدة
        public IFormFile? LogoFile { get; set; }

        // ✅ لعرض الصورة الحالية (اختياري)
        public string? CurrentLogoPath { get; set; } = "~/images/default-project.png";
    }
}
