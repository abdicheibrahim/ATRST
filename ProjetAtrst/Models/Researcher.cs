namespace ProjetAtrst.Models
{
    public class Researcher:Person
    {
        [Required]
        public bool Status { get; set; } = false;
        [Required]
        public string RegisterDate { get; set; }=string.Empty;
        public int ProjectId { get; set; } = -1;
        public Project Project { get; set; } = default!;
        public int AdminId { get; set; } = -1;
        public Admin Admin  { get; set; } =default!;
    }
}
 