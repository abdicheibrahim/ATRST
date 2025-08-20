using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetAtrst.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetAtrst.ViewModels.Account
{
    public class CompleteProfileViewModel
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

     //   [Required(ErrorMessage = "Establishment is required")]
        [Display(Name = "Establishment")]
        public string Establishment { get; set; } = string.Empty;
        //public string CustomEstablishment { get; set; } = string.Empty;
        //public List<SelectListItem> EstablishmentsList { get; set; } = new();

      //  [Required(ErrorMessage = "Statut is required")]
        public string Statut { get; set; }
       // public string CustomStatut { get; set; }
        //public List<SelectListItem> StatutList { get; set; } = new();

     //   [Required(ErrorMessage = "Grade is required")]
        [Display(Name = "Grade")]
        public string Grade { get; set; } = string.Empty;
        //public string CustomGrade { get; set; } = string.Empty;
        //public List<SelectListItem> GradesList { get; set; } = new();
        
        [Required(ErrorMessage = "Speciality is required")]
        [Display(Name = "Speciality")]
        public string Speciality { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number format")]
        [Display(Name = "Mobile")]
        public string? Mobile { get; set; }

        [Required(ErrorMessage = "Diploma is required")]
        [Display(Name = "Diploma")]
        public string Diploma { get; set; } = string.Empty;

        [Required(ErrorMessage = "Diploma Institution is required")]
        [Display(Name = "Diploma Institution")]
        public string DipInstitution { get; set; } = string.Empty;

        [Required(ErrorMessage = "Diploma Date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Diploma Date")]
        public DateTime DipDate { get; set; }

        public bool IsLeader { get; set; } = false;
        public bool IsMember { get; set; } = false;

        public bool IsCompleted { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;

        // ✅ جديد:
        [Display(Name = "Souhaitez-vous contribuer en tant que partenaire social/économique ?")]
        public bool WantsToContributeAsPartner { get; set; } = false;

        [Display(Name = "Contributions économiques et sociales")]
        [DataType(DataType.MultilineText)]
        public string? SocioEconomicContributions { get; set; }
    }
}
