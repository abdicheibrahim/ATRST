namespace ProjetAtrst.Models
{
    public class ProjectFile
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public string UploadedById { get; set; }
        public ProjectLeader UploadedBy { get; set; }
    }
}
