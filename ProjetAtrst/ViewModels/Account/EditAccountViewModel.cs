using ProjetAtrst.ValidationAttributes;

namespace ProjetAtrst.ViewModels.Account
{
    public class EditAccountViewModel
    {
       // [Required]
        [EmailAddress]
        [Display(Name = "Adresse e-mail")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe actuel")]
        public string? CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nouveau mot de passe")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        [Display(Name = "Confirmer le nouveau mot de passe")]
        public string? ConfirmPassword { get; set; }


        [Display(Name = "Photo de profil")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "Formats acceptés : JPG, JPEG, PNG ")]
        public IFormFile? ProfilePicture { get; set; }

        public string? ExistingProfilePicturePath { get; set; }
    }

}
