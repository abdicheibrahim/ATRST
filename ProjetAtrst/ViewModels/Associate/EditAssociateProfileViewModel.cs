namespace ProjetAtrst.ViewModels.Associate
{
    public class EditAssociateProfileViewModel
    {
        [Display(Name = "Diploma")]
        public string? Diploma { get; set; }
        [Display(Name = "Speciality")]
        public string? Speciality { get; set; }
        [Display(Name = "Participation (Membre associé)")]
        public string? MemberParticipation { get; set; }

    }
}
