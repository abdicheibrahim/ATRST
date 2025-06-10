using ProjetAtrst.Models;

namespace ProjetAtrst.ViewModels
{
    public class SidebarViewModel
    {
        public bool IsCompleted { get; set; }=false;
        public bool IsApproved { get; set; }= false;
        public ProjectMember? ProjectMember { get; set; }
        public ProjectLeader? ProjectLeader { get; set; }

    }
}
