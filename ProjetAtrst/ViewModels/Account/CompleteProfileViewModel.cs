using ProjetAtrst.ViewModels.Associate;
using ProjetAtrst.ViewModels.Partner;
using ProjetAtrst.ViewModels.Researcher;

namespace ProjetAtrst.ViewModels.Account
{
    public class CompleteProfileViewModel
    {
        // Role Type Selection
        [Required(ErrorMessage = "Veuillez sélectionner votre rôle")]
        [Display(Name = "Type de profil")]
        public RoleType RoleType { get; set; }  // Chercheur | Partenaire | Associe

        public EditProfileViewModel PersonalInformation { get; set; }
       
        public ResearcherProfileViewModel Researcher { get; set; }= new ResearcherProfileViewModel();

        public PartnerProfileViewModel Partner { get; set; } = new PartnerProfileViewModel();
        public AssociateProfileViewModel Associate { get; set; } = new AssociateProfileViewModel();

        public bool IsLeader { get; set; } = false;
        public bool IsMember { get; set; } = false;

        public bool IsCompleted { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
    }
}
