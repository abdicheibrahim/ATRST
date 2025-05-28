namespace ProjetAtrst.ViewModel.Researcher
{
    public class ResearcherProfileViewModel
    {
        [MaxLength(50)]
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; } = string.Empty;
        
        [MaxLength(50)]
        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; } = string.Empty;
        [MaxLength(50)]
        [Required(ErrorMessage = "الاسم مطلوب")]
        public string FirstNameAr { get; set; } = default!;
        [MaxLength(50)]
        [Required(ErrorMessage = "اللقب مطلوب")]
        public string LastNameAr { get; set; } = default!;
        [Required(ErrorMessage = "Please select your gender")]
        public string Gender {  get; set; } = default!;
        
        public DateTime Birthday { get; set; }
        public string Establishment { get; set; }
        public string Status { get; set; }
        public string Grade { get; set; }
        public string Speciality { get; set; }
        public string? Mobile { get; set; }
        public string Diploma { get; set; }
        public string DipInstitution { get; set; }
        public DateTime DipDate { get; set; }
    }
}
