namespace ProjetAtrst.Models
{
    public class Admin: Person
    {
        public ICollection<Researcher> Researchers { get; set; } = new List<Researcher>();
        public ICollection<Expert> Experts { get; set; } = new List<Expert>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
