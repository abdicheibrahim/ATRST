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

        [Required(ErrorMessage = "الاسم مطلوب")]
        [MaxLength(50, ErrorMessage = "الاسم يجب أن لا يتجاوز 50 حرفًا")]
        [RegularExpression(@"^[\u0621-\u064A\s]+$", ErrorMessage = "الاسم يجب أن يحتوي على حروف عربية ومسافات فقط")]
        [Display(Name = "الاسم")]
        public string FirstNameAr { get; set; } = string.Empty;

        [Required(ErrorMessage = "اللقب مطلوب")]
        [MaxLength(50, ErrorMessage = "الاسم يجب أن لا يتجاوز 50 حرفًا")]
        [RegularExpression(@"^[\u0621-\u064A\s]+$", ErrorMessage = "اللقب يجب أن يحتوي على حروف عربية ومسافات فقط")]
        [Display(Name = "اللقب")]
        public string LastNameAr { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select your gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birthday is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime Birthday { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        [Display(Name = "Mobile")]
        public string? Mobile { get; set; }
    }
}
