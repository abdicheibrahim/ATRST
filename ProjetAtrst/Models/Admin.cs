namespace ProjetAtrst.Models
{
    public class Admin: ApplicationUser
    {
        public ICollection<Researcher> ApprovedResearchers { get; set; } = new List<Researcher>();
        public ICollection<Project> ApprovedProjects { get; set; } = new List<Project>();
    }
}
