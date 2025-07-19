using ProjetAtrst.Models;

namespace ProjetAtrst.ViewModels.Project
{
    public class ProjectListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
        public DateTime LastActivity { get; set; }
        public Role Role { get; set; }
        public string? LogoPath { get; set; } = "/images/default-project.png";

    }
}
