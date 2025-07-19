using ProjetAtrst.Models;

namespace ProjetAtrst.ViewModels.ProjectMembership
{
    public class ProjectMemberViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public DateTime JoinedAt { get; set; }
    }

}
