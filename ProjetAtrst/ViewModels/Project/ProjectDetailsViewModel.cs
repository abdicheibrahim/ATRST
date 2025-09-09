using ProjetAtrst.Models;

namespace ProjetAtrst.ViewModels.Project
{
    public class ProjectDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime CreationDate { get; set; }
        public string Status { get; set; } = "";
        public Role Role { get; set; } 
    }
}
