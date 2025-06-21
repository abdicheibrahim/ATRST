namespace ProjetAtrst.ViewModels.Dashboard
{
    public class MyLeaderProjectsViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; } = string.Empty;
        public int MemberCount { get; set; }
        public int RelatedNotificationsCount { get; set; }
        public string? ImageUrl { get; set; } // احتياطي لصورة المشروع مستقبلاً
    }

}
