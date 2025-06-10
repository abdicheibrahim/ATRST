using ProjetAtrst.Models;

namespace ProjetAtrst.ViewModels
{
    public class DashboardViewModel
    {
        //public int ProjectsLedCount { get; set; }
        //public int ProjectsMemberCount { get; set; }
        //public bool CanCreateProject { get; set; }
        //public bool CanJoinProjects { get; set; }
        //public List<ProjectSummaryViewModel> LedProjects { get; set; } = new();
        //public List<ProjectSummaryViewModel> MemberProjects { get; set; } = new();

       
        public string UserName { get; set; } 
        public int ProjectsLedCount { get; set; }
        public int ProjectsMemberCount { get; set; }
        public bool CanCreateProject { get; set; }
        public int PendingJoinRequestsCount { get; set; }
        public List<Project> RecentProjects { get; set; } = new();
        public IEnumerable<Project> LedProjects { get; set; } 
        public IEnumerable<Project> MemberProjects { get; set; }

    }
}
