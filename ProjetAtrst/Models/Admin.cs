using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetAtrst.Models
{
    public class Admin
    {
        [Key]
        public string Id { get; set; }

        //[ForeignKey(nameof(Id))]
        public ApplicationUser User { get; set; }
        public ICollection<ApplicationUser> ApprovedUsers { get; set; } = new List<ApplicationUser>();
        public ICollection<Project> ApprovedProjects { get; set; } = new List<Project>();
    }
}
