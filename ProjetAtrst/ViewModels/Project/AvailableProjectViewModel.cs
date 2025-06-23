namespace ProjetAtrst.ViewModels.Project
{
    public class AvailableProjectViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LeaderFullName { get; set; }
        public DateTime CreationDate { get; set; }
        public string LeaderId { get; set; } // ← أضف هذا

    }

}
