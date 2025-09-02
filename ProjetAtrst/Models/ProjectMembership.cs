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

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Role Role { get; set; }   // Leader / Member
        public DateTime JoinedAt { get; set; }



    }
}
