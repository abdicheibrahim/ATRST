using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetAtrst.Models
{
 
 
    public class Researcher
    {
        [Key]
        [Required]
        public string Id { get; set; } = default!;
        [ForeignKey(nameof(Id))]
        [Required]
        public ApplicationUser User { get; set; } = default!;


        public string? Diploma { get; set; }
        public string? Grade { get; set; }
        public string? Speciality { get; set; }
        public string? Establishment { get; set; }
        public List<string>? ParticipationPrograms { get; set; } = new List<string>();
       
    }

}
