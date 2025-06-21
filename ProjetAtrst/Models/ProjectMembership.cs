namespace ProjetAtrst.Models
{
    public enum Role
    {
        Leader,
        Member,
        Viewer
    }
   

    public class ProjectMembership
    {
        public string ResearcherId { get; set; }
        public Researcher Researcher { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public Role Role { get; set; }
        public DateTime JoinedAt { get; set; }

    }
}
