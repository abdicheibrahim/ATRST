using ProjetAtrst.Models;

namespace ProjetAtrst.ViewModels.ProjectMembership
{
    public class ProjectMemberViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public DateOnly JoinedAt { get; set; }
    }

}
