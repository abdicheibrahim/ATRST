namespace ProjetAtrst.ViewModels.Researcher
{
    public class ResearcherViewModel
    {
        [Required]
        public string Id { get; set; } = default!;

        public string? FullName { get; set; }
        public string? Gender { get; set; } = default!;
        public string? ProfilePicturePath { get; set; }
    }
}
