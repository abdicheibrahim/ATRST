namespace ProjetAtrst.DTO
{
    public class AvailableProjectDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? LeaderId { get; set; }
        public string? LeaderFullName { get; set; }
    }

}
