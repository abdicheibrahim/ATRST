namespace ProjetAtrst.ViewModels.Account
{
    public class EditProfileViewModel
    {
        [MaxLength(50)]
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(50)]
        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(50)]
        [Required(ErrorMessage = "الاسم مطلوب")]
        [Display(Name = "الاسم")]
        public string FirstNameAr { get; set; } = string.Empty;

        [MaxLength(50)]
        [Required(ErrorMessage = "اللقب مطلوب")]
        [Display(Name = "اللقب")]
        public string LastNameAr { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select your gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birthday is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime Birthday { get; set; }
    }
}
