namespace ProjetAtrst.DTO
{
    public class ProjectDetailsDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Domain { get; set; }
        public string? Axis { get; set; }
        public string? Theme { get; set; }
        public string? Nature { get; set; }
        public string? PNR { get; set; }
        public string? TRL { get; set; }
        public DateOnly CreationDate { get; set; }
        public string? ImageUrl { get; set; } 
        public string? LeaderId { get; set; }
        public string? LeaderFullName { get; set; }
    }
}
