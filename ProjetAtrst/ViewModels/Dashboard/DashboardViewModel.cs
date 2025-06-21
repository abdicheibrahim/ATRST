namespace ProjetAtrst.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public List<MyLeaderProjectsViewModel> LeaderProjects { get; set; } = new();
        public List<MyJoinedProjectsViewModel> JoinedProjects { get; set; } = new();
    }
}
