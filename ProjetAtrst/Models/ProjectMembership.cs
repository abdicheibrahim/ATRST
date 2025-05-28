namespace ProjetAtrst.Models
{
    public class ProjectMembership
    {
        public string MemberId { get; set; }
        public ProjectMember Member { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public DateTime JoinedAt { get; set; }
    }
}
