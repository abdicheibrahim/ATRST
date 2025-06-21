namespace ProjetAtrst.ViewModels.Dashboard
{
    public class MyJoinedProjectsViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; } = string.Empty;
        public string LeaderFullName { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
    }
}
