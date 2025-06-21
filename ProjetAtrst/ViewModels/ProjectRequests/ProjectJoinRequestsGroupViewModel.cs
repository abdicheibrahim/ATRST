namespace ProjetAtrst.ViewModels.ProjectRequests
{
    public class ProjectJoinRequestsGroupViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; } = string.Empty;

        public List<JoinRequestItemViewModel> JoinRequests { get; set; } = new();
    }

}
