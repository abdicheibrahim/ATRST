namespace ProjetAtrst.ViewModels.Account
{
    public class EditProfileViewModel
    {
        [MaxLength(50)]
        [Required(ErrorMessage = "Le prénom est requis")]
        [Display(Name = "Prénom")]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(50)]
        [Required(ErrorMessage = "Le nom de famille est requis")]
        [Display(Name = "Nom de famille")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "الاسم مطلوب")]
        [MaxLength(50, ErrorMessage = "يجب ألا يتجاوز الاسم 50 حرفًا")]
        [RegularExpression(@"^[\u0621-\u064A\s]+$", ErrorMessage = "يجب أن يحتوي الاسم على حروف عربية ومسافات فقط")]
        [Display(Name = "الاسم")]
        public string FirstNameAr { get; set; } = string.Empty;

        [Required(ErrorMessage = "اللقب مطلوب")]
        [MaxLength(50, ErrorMessage = "يجب ألا يتجاوز اللقب 50 حرفًا")]
        [RegularExpression(@"^[\u0621-\u064A\s]+$", ErrorMessage = "يجب أن يحتوي اللقب على حروف عربية ومسافات فقط")]
        [Display(Name = "اللقب")]
        public string LastNameAr { get; set; } = string.Empty;

        [Required(ErrorMessage = "Veuillez sélectionner votre genre")]
        [RegularExpression("^(Homme|Femme)$", ErrorMessage = "Veuillez sélectionner un genre valide : Homme ou Femme")]
        [Display(Name = "Genre")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "La date de naissance est requise")]
        [DataType(DataType.Date)]
        [Display(Name = "Date de naissance")]
        public DateOnly Birthday { get; set; }

        [Phone(ErrorMessage = "Format de numéro de téléphone invalide")]
        [Display(Name = "Mobile")]
        public string? Mobile { get; set; }
    }
}
