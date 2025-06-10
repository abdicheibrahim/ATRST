using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetAtrst.Models
{
    public class Expert 
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey(nameof(Id))]
        public Researcher Researcher { get; set; }
        public ICollection<ProjectEvaluation> Evaluations { get; set; } = new List<ProjectEvaluation>();
       
    }
}
